using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    
    private Transform target;
    private Rigidbody2D rb;
    public float velocity;
    public float avoidVelocity;
    public Vector2 targetDirection = Vector2.zero;

    float maxSeeAhead = 1.0f;
    float xSize, ySize;

    Vector3 topLeft, topRight,bottomLeft, bottomRight, center, bottomMid, topMid;

    public const float avoidingPercentage = 0.8f;
    public float followingPercentage = 1 - avoidingPercentage;


    void Start()
    {
        target = FindObjectOfType<Player_Controller>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();

        SetInitialBoundingBoxInfo();
    }

    void FixedUpdate()
    {
        CreateVirtualBoundingBox();
        CheckForCollisionDetected();
    }

    private void SetInitialBoundingBoxInfo()
    {
        /* Calculate some data that will be used to create the bounding box around the agent */
        float currentZRotation = transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        xSize = GetComponent<SpriteRenderer>().bounds.size.x;
        ySize = GetComponent<SpriteRenderer>().bounds.size.y;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentZRotation));
    }

    private void CreateVirtualBoundingBox()
    {
        center = transform.position;

        if (targetDirection.y > 0 && targetDirection.y > targetDirection.x)
        {

        //UP
            /* Create the bounding box around the sprite for collision detection */
            //bottomRight = transform.position + (transform.right * (xSize / 2)) + (-transform.up * (ySize / 2));
            //bottomLeft = transform.position + (-transform.right * (xSize / 2)) + (-transform.up * (ySize / 2));

            topRight = transform.position + ((transform.right * (xSize/2)) + (transform.up * maxSeeAhead));
            topLeft = transform.position + (-transform.right * (xSize/2)) + (transform.up * maxSeeAhead);

            bottomMid = transform.position + (transform.right * 0) + (-transform.up * (ySize/2));
            topMid = transform.position + ((transform.right * 0) + (transform.up * (maxSeeAhead )));


        }
        //LEFT
        else if (targetDirection.x < 0 && (-(targetDirection.x) > targetDirection.y))
        {
            //bottomRight = transform.position + (-transform.up * (xSize / 2)) + (transform.right * (ySize / 2));
            //bottomLeft = transform.position + (transform.up * (xSize / 2)) + (transform.right * (ySize / 2));

            topRight = transform.position + ((-transform.up * (xSize / 2)) + (-transform.right * maxSeeAhead));
            topLeft = transform.position + (transform.up * (xSize / 2)) + (-transform.right * maxSeeAhead);

            bottomMid = transform.position + (-transform.right * 0) + (-transform.right * (ySize / 2));
            topMid = transform.position + ((transform.right * 0) + (-transform.right * (maxSeeAhead)));

        }
        //DOWN
        else if (targetDirection.y < 0 && (-(targetDirection.x) > targetDirection.y))
        {  
            //bottomRight = transform.position + (-transform.right * (xSize / 2)) + (transform.up * (ySize / 2));
            //bottomLeft = transform.position + (transform.right * (xSize / 2)) + (transform.up * (ySize / 2));

            topRight = transform.position + ((-transform.right * (xSize / 2)) + (-transform.up * maxSeeAhead));
            topLeft = transform.position + (transform.right * (xSize / 2)) + (-transform.up * maxSeeAhead);

            bottomMid = transform.position + (transform.right * 0) + (transform.up * (ySize / 2));
            topMid = transform.position + ((transform.right * 0) + (-transform.up * (maxSeeAhead)));

        }
        //RIGHT
        else if (targetDirection.x > 0 && targetDirection.x > targetDirection.y)
        {
            //bottomRight = transform.position + (transform.up * (xSize / 2)) + (-transform.right * (ySize / 2));
            //bottomLeft = transform.position + (-transform.up * (xSize / 2)) + (-transform.right * (ySize / 2));

            topRight = transform.position + ((transform.up * (xSize / 2)) + (transform.right * maxSeeAhead));
            topLeft = transform.position + (-transform.up * (xSize / 2)) + (transform.right * maxSeeAhead);

            bottomMid = transform.position + (transform.right * 0) + (transform.right * (ySize / 2));
            topMid = transform.position + ((transform.right * 0) + (transform.right * (maxSeeAhead)));
        }
        topRight = Quaternion.Euler(0, 0, -45f) * (this.transform.position + this.transform.up);
        topLeft = Quaternion.Euler(0, 0, 45f) * (this.transform.position + this.transform.up);
        topMid = Quaternion.Euler(0, 0, 0f) * (this.transform.position + this.transform.up);
        //Draw Raycast Lines
        //Debug.DrawRay(bottomRight, (bottomLeft - bottomRight), Color.green);
        Debug.DrawRay(topRight, (topLeft - topRight), Color.green);

        //Debug.DrawRay(bottomRight, (topRight - bottomRight), Color.green);
        //Debug.DrawRay(bottomLeft, (topLeft - bottomLeft), Color.green);

        Debug.DrawRay(center, (topRight - center), Color.red);
        Debug.DrawRay(center, (topLeft - center), Color.red);

        Debug.DrawRay(center, (topMid - center), Color.green);
        

    }

    private void CheckForCollisionDetected()
    {
        RaycastHit2D[] hit2D = new RaycastHit2D[3];
        LayerMask mask = LayerMask.GetMask("object");


        /* 2 raycasts are used for this, one points from the bottom left corner to the top left corner of the agent and
        the other from the bottom right to the top right */
        hit2D[0] = Physics2D.Raycast(bottomLeft, topLeft - bottomLeft, maxSeeAhead,mask);
        hit2D[1] = Physics2D.Raycast(bottomRight, topRight - bottomRight, maxSeeAhead,mask);
        hit2D[2] = Physics2D.Raycast(bottomMid, topMid - bottomMid, maxSeeAhead,mask);
      



        Vector2 dirOfMovementToAvoidObstacle;

        if (hit2D[0])
        {
            /* if a collision was detected on the left side of the bounding box, the direction of movement (to
            steer away from the obstacle) will be to the right. */
            dirOfMovementToAvoidObstacle = topRight - hit2D[0].collider.transform.position;

            /* Make the direction of vector to avoid obtacle, point away from it as much as possible to ensure the obstacle doesnt collide with it
             This can obviously be changed to make your own direction of movement when an obstacle is detected.*/
            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[0].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * (avoidVelocity * avoidingPercentage));
            rb.AddForce(targetDirection.normalized * (velocity * followingPercentage));

            Debug.DrawRay(hit2D[0].collider.transform.position, topRight - hit2D[0].collider.transform.position, Color.white);
            
        }

        else if (hit2D[1])
        {
         
            dirOfMovementToAvoidObstacle = topLeft - hit2D[1].collider.transform.position;

            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[1].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * (avoidVelocity * avoidingPercentage));
            rb.AddForce(targetDirection.normalized * (velocity * followingPercentage));


            Debug.DrawRay(hit2D[1].collider.transform.position, topLeft - hit2D[1].collider.transform.position, Color.white);
            
        }
        else if (hit2D[2])
        {
            dirOfMovementToAvoidObstacle = bottomMid - hit2D[2].collider.transform.position;
            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[2].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * (velocity-33) );

            Debug.DrawRay(hit2D[2].collider.transform.position, bottomMid - hit2D[2].collider.transform.position, Color.white);
        }

        /* If no obstacle was detected, then just steer it towards it's current velocity */
        else
        {
            targetDirection = target.transform.position - this.transform.position;
            rb.AddForce(targetDirection.normalized * velocity);
        }
    }
}
