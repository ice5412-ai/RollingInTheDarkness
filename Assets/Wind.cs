using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Wind : MonoBehaviour
{
    [SerializeField] float WindForce = 15;
    [SerializeField] float Range = 10;
    [SerializeField] ForceMode _forceMode = ForceMode.Force;
    [SerializeField] AudioSource WindOnMic = null;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, Range))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(transform.position, transform.up * hit.distance, Color.green);
                if (Application.isPlaying)
                {
                    if (WindOnMic != null)
                    {
                        WindOnMic.enabled = true;
                    }
                    hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * WindForce * Time.deltaTime * 100, _forceMode);
                }
            }
            else
            {
                if (Application.isPlaying)
                {
                    if (WindOnMic != null)
                    {
                        WindOnMic.enabled = false;
                    }
                }
                Debug.DrawRay(transform.position, transform.up * hit.distance, Color.yellow);
            }
        }
        else
        {
            if (Application.isPlaying)
            {
                if (WindOnMic != null)
                {
                    WindOnMic.enabled = false;
                }
            }
            Debug.DrawRay(transform.position, transform.up * Range, Color.red);
        }
    }
}
