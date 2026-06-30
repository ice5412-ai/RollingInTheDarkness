using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObstacle : MonoBehaviour
{
    Material material;
    float intensity = 0;
    public List<GameObject> Collided = new List<GameObject>();
    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        if (GetComponent<FixedJoint>() == null)
        {
            changeMat(false);
        }
        else
        {
            changeMat(true);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "SpringDoor")
        {
            if (GetComponent<FixedJoint>() == null)
            {
                Collided.Add(other.gameObject);
                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = other.rigidbody;
                fixedJoint.enableCollision = true;
                if (other.gameObject.GetComponent<StickySpringDoor>())
                {
                    if (!other.gameObject.GetComponent<StickySpringDoor>().Collided.Contains(gameObject))
                    {
                        other.gameObject.GetComponent<StickySpringDoor>().Collided.Add(gameObject);
                    }
                }
            }
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            if (GetComponent<FixedJoint>())
            {
                Destroy(GetComponent<FixedJoint>());
                foreach (GameObject cld in Collided)
                {
                    if (cld.GetComponent<StickySpringDoor>())
                    {
                        if (cld.GetComponent<StickySpringDoor>().Collided.Contains(gameObject))
                        {
                            cld.GetComponent<StickySpringDoor>().Collided.Remove(gameObject);
                            Collided.Remove(cld);
                            break;
                        }
                    }
                }
            }
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

