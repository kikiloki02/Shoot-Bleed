using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemy : MonoBehaviour
{
    public GameObject _pivot;
    public float _speed;

    void Update()
    {
        transform.RotateAround(_pivot.transform.position, Vector3.forward, _speed);
    }
}
