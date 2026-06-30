using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class LoseCondition : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume = null;
    [SerializeField] Text StatusText = null;
    [SerializeField] GameObject DestroyedParticle = null;
    [SerializeField] GameObject PureCrystal = null;
    WinCondition winCondition;
    private ColorGrading colorGrading;
    Coroutine dead;
    private void Awake()
    {
        volume = GameObject.FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGrading);
        colorGrading.saturation.value = 0;

        colorGrading = GameObject.FindObjectOfType<ColorGrading>();
        if (StatusText == null)
        {
            StatusText = GameObject.Find("StatusText").GetComponent<Text>();
        }
        if (PureCrystal == null)
        {
            PureCrystal = GameObject.FindObjectOfType<WinCondition>().gameObject;
            winCondition = PureCrystal.GetComponent<WinCondition>();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!winCondition.Winning)
            {
                GameObject.FindObjectOfType<GravityController>()._TopView = false;
                Instantiate(DestroyedParticle, other.transform.position, Quaternion.identity);
                winCondition.textCountdown = 5;
                winCondition.switchingText = 8;
                if (!winCondition.hideInterface)
                {
                    StatusText.enabled = true;
                }
                StatusText.color = Color.white;
                StatusText.text = "You have been destroyed! We are sending you back to the last check point...";
                other.gameObject.SetActive(false);
                winCondition.ReloadSceneWithDelay(5f);
            }
        }
    }
}
