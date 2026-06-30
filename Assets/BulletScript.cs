using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class BulletScript : MonoBehaviour
{
    [SerializeField] GameObject ParticleOnDestroy = null;
    private void OnCollisionEnter(Collision other)
    {
        LeanPool.Spawn(ParticleOnDestroy, transform.position, Quaternion.identity);
        LeanPool.Despawn(this.gameObject);
    }
}
