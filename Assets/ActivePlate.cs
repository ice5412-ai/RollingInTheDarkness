using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlate : MonoBehaviour
{
    [SerializeField] GameObject TheLight = null;
    private float tragetIntensity;
    [SerializeField] GameObject MainLight = null;
    [SerializeField] GameObject FollowLight = null;
    [SerializeField] GameObject _onDestroyParticle = null;
    [SerializeField] Color TragetColor = Color.white;
    [SerializeField] Text StatusText = null;
    [SerializeField] GameObject PureCrystal = null;
    WinCondition winCondition = null;
    private float _Scale = .5f;
    private void Awake()
    {
        StatusText.enabled = false;
        TheLight.SetActive(false);
        winCondition = PureCrystal.GetComponent<WinCondition>();
        winCondition.remainActivePlate += 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            winCondition.SpawnPoint.transform.position = transform.position;
            winCondition.remainActivePlate -= 1;
            winCondition.textCountdown = 6;
            winCondition.switchingText = 8;
            if (!winCondition.hideInterface)
            {
                StatusText.enabled = true;
            }
            TheLight.SetActive(true);
            GameObject Destroyed = Instantiate(_onDestroyParticle, transform.position, Quaternion.identity);
            Color currentColor = other.GetComponent<MeshRenderer>().material.color;
            currentColor = currentColor + (TragetColor / 6);
            MainLight.GetComponent<Light>().color = currentColor;
            FollowLight.GetComponent<Light>().color = currentColor;
            StatusText.text = "You've picked " + this.gameObject.name + ".";

            PureCrystal.GetComponent<MeshRenderer>().material.color = currentColor;
            PureCrystal.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", currentColor);
            PureCrystal.GetComponent<ParticleSystemRenderer>().material.color = currentColor;
            PureCrystal.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", currentColor);

            other.GetComponent<MeshRenderer>().material.color = currentColor;
            other.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", currentColor / 3);
            other.GetComponent<ParticleSystemRenderer>().material.color = currentColor;
            other.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", currentColor / 3);

            if (winCondition.remainActivePlate <= 0)
            {
                StatusText.text = StatusText.text + "\n Pure Crystal is appear. Now take it!";
            }
            else if (winCondition.remainActivePlate == 1)
            {
                StatusText.text = StatusText.text + "\n" + winCondition.remainActivePlate + " Crystal remaining.";
            }
            else
            {
                StatusText.text = StatusText.text + "\n" + winCondition.remainActivePlate + " Crystals remaining.";
            }
            StatusText.GetComponent<Text>().color = TragetColor;
            if (!winCondition.hideInterface)
            {
                StatusText.enabled = true;
            }
            GetComponent<ParticleSystem>().Stop();
            GetComponent<Collider>().enabled = false;
            StartCoroutine(EndPlate());
        }
    }
    private IEnumerator EndPlate()
    {
        while (true)
        {
            yield return Time.deltaTime;
            if (_Scale > 0)
            {
                _Scale -= Time.deltaTime;
                this.gameObject.transform.localScale = new Vector3(_Scale, _Scale, _Scale);
            }
            else
            {
                _Scale = 0;
                if (GetComponent<ParticleSystem>().isStopped)
                {
                    winCondition.DeactiveText(5);
                    Destroy(gameObject, 6);
                }
            }
        }
    }
}
