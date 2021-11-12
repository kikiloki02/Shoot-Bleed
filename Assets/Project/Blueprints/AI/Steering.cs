using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public float followSpeed = 15f;
    public float slowdownDistance = 1f;
    public Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;


    private Vector2 targetPosition;
    private Vector2 playerDistance;
    private Vector2 desiredVelocity;
    private Vector2 steering;

    

    private float slowdownFactor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerDistance = (targetPosition - (Vector2)transform.position);
        desiredVelocity = playerDistance.normalized * followSpeed;
        rb.AddForce(desiredVelocity);
        steering = desiredVelocity - velocity;
        rb.AddForce(steering);
        
       

        velocity += steering * Time.deltaTime;
       // slowdownFactor = Mathf.Clamp01(playerDistance.magnitude/slowdownDistance);
       // velocity *= slowdownFactor;
        //transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
