using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySpringDoor : MonoBehaviour
{
    Material material;
    float intensity = 0;
    public List<GameObject> Collided = new List<GameObject>();
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Collided.Count == 0)
        {
            changeMat(false);
        }
        else
        {
            changeMat(true);
        }
    }
    public void changeMat(bool sticked)
    {
        if (sticked)
        {
            if (intensity < .5)
            {
                intensity += Time.deltaTime;
            }
        }
        else
        {
            if (intensity > 0)
            {
                intensity -= Time.deltaTime;
            }
        }
        material.SetColor("_EmissionColor", new Color(intensity, intensity, intensity));
    }
}

