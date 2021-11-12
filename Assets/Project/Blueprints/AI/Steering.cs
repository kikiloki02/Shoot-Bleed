using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public float velocity;
    public float slowdownDistance = 1f;
    public Rigidbody2D rb;


    private Vector2 targetPosition;
    private Vector2 playerDistance;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(playerDistance.x, playerDistance.y, 0) * velocity);
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerDistance = (targetPosition - (Vector2)transform.position);

    }

}
