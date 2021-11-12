using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehavior
{
    [SerializeField]
    Transform target;

    void Update()
    {
        Steer(target.position);
        ApplySteeringToMotion();
    }
}
