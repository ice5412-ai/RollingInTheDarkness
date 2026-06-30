using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MachineGun : MonoBehaviour
{
    [SerializeField] public Transform[] firePoint = null;
    [SerializeField] public GameObject Bullets = null;
    [SerializeField] public float barrageInterval = 2;
    [SerializeField] public float WhirlingTime = 1.5f;
    [SerializeField] public float barrageDelay = 4;
    [SerializeField] public float fireRate = .1f;
    [SerializeField] public float fireForce = 10;
    [SerializeField] public ConstantForce _constantForce = null;
    Coroutine Firing = null;
    Coroutine Whirling = null;
    AudioSource audioSource;
    [SerializeField] public AudioClip gunshotSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(BarrageFireInterval());
    }

    public IEnumerator BarrageFireInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(barrageDelay);
            Whirling = StartCoroutine(whirling());
            //Debug.Log("Whirling: " + Time.time);

            yield return new WaitForSeconds(WhirlingTime);
            Firing = StartCoroutine(BarrageFire());
            StopCoroutine(Whirling);
            //Debug.Log("Barrage Fire: " + Time.time);

            yield return new WaitForSeconds(barrageInterval);
            _constantForce.relativeTorque = new Vector3(0, 0, 0);
            StopCoroutine(Firing);
            //Debug.Log("Cooldown: " + Time.time);
        }
    }
    public IEnumerator whirling()
    {
        float whirlingSpeed = 0;
        float whirlingVelocity = 0;
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            whirlingSpeed = Mathf.SmoothDamp(whirlingSpeed, 100, ref whirlingVelocity, WhirlingTime);
            _constantForce.relativeTorque = new Vector3(0, whirlingSpeed, 0);
        }
    }

    public IEnumerator BarrageFire()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            if (i < firePoint.Length)
            {
                GameObject Bullet = LeanPool.Spawn(Bullets, firePoint[i].position, Quaternion.identity);
                audioSource.PlayOneShot(gunshotSound);
                Rigidbody rb = Bullet.GetComponent<Rigidbody>();
                TrailRenderer tr = Bullet.GetComponentInChildren<TrailRenderer>();
                tr.Clear();
                rb.velocity = Vector3.zero;
                rb.AddForce(fireForce * -transform.forward, ForceMode.Impulse);
                //Debug.Log("_firerate " + Time.time);
                LeanPool.Despawn(Bullet, 3);
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }
}
