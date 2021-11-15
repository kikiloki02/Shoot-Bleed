using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Rigidbody2D rb;
    public float velocity = 10f;
    private Vector3 vector3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        vector3 = target.transform.position - this.transform.position;
        rb.AddForce(vector3.normalized * velocity);

    }    
}
