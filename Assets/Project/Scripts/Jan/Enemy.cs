using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _healthValue;
    public int _attackValue;
    public int _movementSpeed;

    public Collider2D _attackDetectionZone;

// public
    // virtual void Attack1() = 0;
}