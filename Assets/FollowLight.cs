using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowLight : MonoBehaviour
{
    [SerializeField] public GameObject _Player;
    [SerializeField] public float Offset = 10;

    private void FixedUpdate()
    {
        if (_Player != null)
        {
            this.transform.position = _Player.transform.position + Vector3.up * Offset;
        }
    }
}
