using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    private GameObject player;
    private Transform target;
    private Rigidbody2D rb;
    public float velocity;
    public float avoidVelocity;

    public Vector3 targetDirection = Vector3.zero;
    public Vector3 RightPerpendicularTargetDirection = Vector3.zero;
    public Vector3 LeftPerpendicularTargetDirection = Vector3.zero;

    float maxSeeAhead = 1.0f;
    float xSize, ySize;

    Vector3 topLeft, topRight,bottomLeft, bottomRight, center, bottomMid, topMid, centerleft , centerRight;

    public const float avoidingPercentage = 0.9f;
    public float followingPercentage = 1 - avoidingPercentage;


    void Start()
    {
        player = FindObjectOfType<Player_Controller>().gameObject;
        rb = GetComponent<Rigidbody2D>();

        SetInitialBoundingBoxInfo();
    }
    private void Update()
    {
        target = player.transform;
        targetDirection = player.transform.position - this.transform.position;

        //RightPerpendicularTargetDirection = new Vector3(targetDirection.y, -targetDirection.x, targetDirection.z);
        //LeftPerpendicularTargetDirection = new Vector3(-targetDirection.y, targetDirection.x, targetDirection.z);

    }

    void FixedUpdate()
    {

        CreateVirtualBoundingBox();
        CheckForCollisionDetected();
    }

    private void SetInitialBoundingBoxInfo()
    {
        /* Calculate some data that will be used to create the bounding box around the agent */
        //float currentZRotation = transform.eulerAngles.z;
        //transform.rotation = Quaternion.Euler(Vector3.zero);
        xSize = GetComponent<SpriteRenderer>().bounds.size.x;
        ySize = GetComponent<SpriteRenderer>().bounds.size.y;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentZRotation));
    }

    private void CreateVirtualBoundingBox()
    {
        center = this.transform.position;
        topMid = transform.position + targetDirection.normalized * maxSeeAhead;
        topRight = transform.position + (targetDirection.normalized + RightPerpendicularTargetDirection.normalized) * (maxSeeAhead); 
        topLeft = transform.position + (targetDirection.normalized + LeftPerpendicularTargetDirection.normalized) * (maxSeeAhead);
        centerleft = new Vector3(this.transform.position.x - 4, this.transform.position.y, this.transform.position.z);
        centerRight = new Vector3(this.transform.position.x + 4, this.transform.position.y, this.transform.position.z);

        //if (targetDirection.y > 0 && targetDirection.y > targetDirection.x)
        //{

        ////UP
        //    /* Create the bounding box around the sprite for collision detection */
        //    //bottomRight = transform.position + (transform.right * (xSize / 2)) + (-transform.up * (ySize / 2));
        //    //bottomLeft = transform.position + (-transform.right * (xSize / 2)) + (-transform.up * (ySize / 2));

        //    topRight = transform.position + ((transform.right * (xSize/2)) + (transform.up * maxSeeAhead));
        //    topLeft = transform.position + (-transform.right * (xSize/2)) + (transform.up * maxSeeAhead);

        //    bottomMid = transform.position + (transform.right * 0) + (-transform.up * (ySize/2));
        //    topMid = transform.position + ((transform.right * 0) + (transform.up * (maxSeeAhead )));


        //}
        ////LEFT
        //else if (targetDirection.x < 0 && (-(targetDirection.x) > targetDirection.y))
        //{
        //    //bottomRight = transform.position + (-transform.up * (xSize / 2)) + (transform.right * (ySize / 2));
        //    //bottomLeft = transform.position + (transform.up * (xSize / 2)) + (transform.right * (ySize / 2));

        //    topRight = transform.position + ((-transform.up * (xSize / 2)) + (-transform.right * maxSeeAhead));
        //    topLeft = transform.position + (transform.up * (xSize / 2)) + (-transform.right * maxSeeAhead);

        //    bottomMid = transform.position + (-transform.right * 0) + (-transform.right * (ySize / 2));
        //    topMid = transform.position + ((transform.right * 0) + (-transform.right * (maxSeeAhead)));

        //}
        ////DOWN
        //else if (targetDirection.y < 0 && (-(targetDirection.x) > targetDirection.y))
        //{  
        //    //bottomRight = transform.position + (-transform.right * (xSize / 2)) + (transform.up * (ySize / 2));
        //    //bottomLeft = transform.position + (transform.right * (xSize / 2)) + (transform.up * (ySize / 2));

        //    topRight = transform.position + ((-transform.right * (xSize / 2)) + (-transform.up * maxSeeAhead));
        //    topLeft = transform.position + (transform.right * (xSize / 2)) + (-transform.up * maxSeeAhead);

        //    bottomMid = transform.position + (transform.right * 0) + (transform.up * (ySize / 2));
        //    topMid = transform.position + ((transform.right * 0) + (-transform.up * (maxSeeAhead)));

        //}
        ////RIGHT
        //else if (targetDirection.x > 0 && targetDirection.x > targetDirection.y)
        //{
        //    //bottomRight = transform.position + (transform.up * (xSize / 2)) + (-transform.right * (ySize / 2));
        //    //bottomLeft = transform.position + (-transform.up * (xSize / 2)) + (-transform.right * (ySize / 2));

        //    topRight = transform.position + ((transform.up * (xSize / 2)) + (transform.right * maxSeeAhead));
        //    topLeft = transform.position + (-transform.up * (xSize / 2)) + (transform.right * maxSeeAhead);

        //    bottomMid = transform.position + (transform.right * 0) + (transform.right * (ySize / 2));
        //    topMid = transform.position + ((transform.right * 0) + (transform.right * (maxSeeAhead)));
        //}

        //float rotZ = Mathf.Atan2(targetDirection.normalized.y, targetDirection.normalized.x) * Mathf.Rad2Deg;

        //topRight = Quaternion.Euler(0, 0, (rotZ+90)) * (target.transform.position + target.transform.up);
        //topLeft = Quaternion.Euler(0, 0, (rotZ-90)) * (target.transform.position + target.transform.up);
        //topMid = Quaternion.Euler(0, 0, 0f) * (target.transform.position + target.transform.up);

        //Draw Raycast Lines
        //Debug.DrawRay(bottomRight, (bottomLeft - bottomRight), Color.green);
        //Debug.DrawRay(topRight, (topLeft - topRight), Color.green);

        //Debug.DrawRay(bottomRight, (topRight - bottomRight), Color.green);
        //Debug.DrawRay(bottomLeft, (topLeft - bottomLeft), Color.green);

        //Debug.DrawRay(center, (topRight - center), Color.red);
        //Debug.DrawRay(center, (topLeft - center), Color.red);

        Debug.DrawRay(center, (topMid - center), Color.green);
        Debug.DrawRay(center, (topRight - center), Color.red);
        Debug.DrawRay(center, (topLeft - center), Color.yellow);
        Debug.DrawRay(center, (centerleft - center), Color.white);
        // Debug.DrawRay(center, (LeftPerpendicularTargetDirection.normalized - center), Color.white);
        

    }

    private void CheckForCollisionDetected()
    {
        RaycastHit2D[] hit2D = new RaycastHit2D[3];
        LayerMask mask = LayerMask.GetMask("object");


        /* 2 raycasts are used for this, one points from the bottom left corner to the top left corner of the agent and
        the other from the bottom right to the top right */
        hit2D[0] = Physics2D.Raycast(center, topLeft - center, (maxSeeAhead),mask);
        hit2D[1] = Physics2D.Raycast(center, topRight - center, (maxSeeAhead),mask);
        hit2D[2] = Physics2D.Raycast(center, topMid - center, maxSeeAhead,mask);
      



        Vector2 dirOfMovementToAvoidObstacle;

        if (hit2D[0])
        {
            /* if a collision was detected on the left side of the bounding box, the direction of movement (to
            steer away from the obstacle) will be to the right. */
            dirOfMovementToAvoidObstacle = topRight - hit2D[0].collider.transform.position;

            /* Make the direction of vector to avoid obstacle, point away from it as much as possible to ensure the obstacle doesnt collide with it
             This can obviously be changed to make your own direction of movement when an obstacle is detected.*/
            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[0].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * (avoidVelocity * avoidingPercentage));
            rb.AddForce(targetDirection.normalized * (velocity * followingPercentage));




            Debug.DrawRay(hit2D[0].collider.transform.position, topRight - hit2D[0].collider.transform.position, Color.white);
            Debug.DrawRay(topLeft, topRight - hit2D[0].collider.transform.position, Color.blue);
            
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
            dirOfMovementToAvoidObstacle = center - hit2D[2].collider.transform.position;
            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[2].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * (avoidVelocity * avoidingPercentage));
            rb.AddForce(targetDirection.normalized * ((velocity+20) * followingPercentage));

            Debug.DrawRay(hit2D[2].collider.transform.position, bottomMid - hit2D[2].collider.transform.position, Color.white);
        }

        /* If no obstacle was detected, then just steer it towards it's current velocity */
        else
        {
            rb.AddForce(targetDirection.normalized * velocity);
        }
    }
}
