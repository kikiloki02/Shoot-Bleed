using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Rigidbody2D rb;
    //public Collider2D col;
    public float velocity;
    public float avoidVelocity;
    public Vector2 vector2 = Vector2.zero;

    float maxSeeAhead = 3;
    float xSize, ySize;

    Vector3 topLeft, topRight, bottomLeft, bottomRight, center;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //col = GetComponent<Collider2D>();

        SetInitialBoundingBoxInfo();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Object"))
    //    {
    //        vector2.Set(0, 1);
    //        Debug.Log("changing dir");
    //        Movement();           
    //    }
    //    else if (collision.CompareTag("Player")){
    //        vector2 = target.transform.position - this.transform.position;
    //        Debug.Log("following player");
    //        Movement();
    //    }
    //    else
    //    {
    //        vector2= Vector2.zero;
    //    }
    //}

    void FixedUpdate()
    {
        //OnTriggerEnter2D(col);
        // Movement();
        CreateVirtualBoundingBox();
        CheckForCollisionDetected();

    }    

    private void Movement()
    {
        rb.AddForce(vector2.normalized * velocity);
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
        center = transform.position + (-transform.right * 0) + (-transform.up * 0);

        if (vector2.y > 0)
        {
            /* Create the bounding box around the sprite for collision detection */
            bottomRight = transform.position + (transform.right * (xSize / 2)) + (-transform.up * (ySize / 2));
            bottomLeft = transform.position + (-transform.right * (xSize / 2)) + (-transform.up * (ySize / 2));

            topRight = transform.position + ((transform.right * (xSize)) + (transform.up * maxSeeAhead));
            topLeft = transform.position + (-transform.right * (xSize)) + (transform.up * maxSeeAhead);

            // GIRA DEFORMANT-SE
            //topRight = transform.position + ((transform.right * (xSize)) + (target.position.normalized * maxSeeAhead));
            //topLeft = transform.position + (-transform.right * (xSize)) + (target.position.normalized * maxSeeAhead);

            // GIRA EL SPRITE
            //this.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(bottomRight, target.position));
            //this.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(bottomLeft, target.position));

            // AIXO ERA LO BUENO
            // Debug.DrawRay(bottomRight, (topRight - bottomRight), Color.green);
            // Debug.DrawRay(bottomLeft, (topLeft - bottomLeft), Color.green);

        }

        else if (vector2.x < 0)
        {
            bottomRight = transform.position + (-transform.up * (xSize / 2)) + (transform.right * (ySize / 2));
            bottomLeft = transform.position + (transform.up * (xSize / 2)) + (transform.right * (ySize / 2));

            topRight = transform.position + ((-transform.up * (xSize)) + (-transform.right * maxSeeAhead));
            topLeft = transform.position + (transform.up * (xSize)) + (-transform.right * maxSeeAhead);
        }
        //ESTA A MEDIAS----------------------------------------------------------------------------------------------------------------
        else if (vector2.y < 0 && vector2.x >vector2.y)
        {
            bottomRight = transform.position + (-transform.right * (xSize / 2)) + (transform.up * (ySize / 2));
            bottomLeft = transform.position + (transform.right * (xSize / 2)) + (transform.up * (ySize / 2));

            topRight = transform.position + ((-transform.right * (xSize)) + (-transform.up * maxSeeAhead));
            topLeft = transform.position + (transform.right * (xSize)) + (-transform.up * maxSeeAhead);

        }
        //------------------------------------------------------------------------------------------------------------------------------
        else if (vector2.x > 0)
        {
            bottomRight = transform.position + (transform.up * (xSize / 2)) + (-transform.right * (ySize / 2));
            bottomLeft = transform.position + (-transform.up * (xSize / 2)) + (-transform.right * (ySize / 2));

            topRight = transform.position + ((transform.up * (xSize)) + (transform.right * maxSeeAhead));
            topLeft = transform.position + (-transform.up * (xSize)) + (transform.right * maxSeeAhead);
        }

        Debug.DrawRay(bottomRight, (bottomLeft - bottomRight), Color.green);
        Debug.DrawRay(topRight, (topLeft - topRight), Color.green);

        Debug.DrawRay(bottomRight, (topRight - bottomRight), Color.green);
        Debug.DrawRay(bottomLeft, (topLeft - bottomLeft), Color.green);

        //My test
        Debug.DrawRay(center, (topRight - center), Color.red);
        Debug.DrawRay(center, (topLeft - center), Color.red);

    }

    private void CheckForCollisionDetected()
    {
        RaycastHit2D[] hit2D = new RaycastHit2D[2];

        /* 2 raycasts are used for this, one points from the bottom left corner to the top left corner of the agent and
        the other from the bottom right to the top right */
        hit2D[0] = Physics2D.Raycast(bottomLeft, topLeft - bottomLeft, maxSeeAhead);
        hit2D[1] = Physics2D.Raycast(bottomRight, topRight - bottomRight, maxSeeAhead);
      



        Vector2 dirOfMovementToAvoidObstacle;

        if (hit2D[0])
        {
            //hit2D[0].transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, target.position-this.transform.position));
            /* if a collision was detected on the left side of the bounding box, the direction of movement (to
            steer away from the obstacle) will be to the right. */
            dirOfMovementToAvoidObstacle = topRight - hit2D[0].collider.transform.position;

            /* Make the direction of vector to avoid obtacle, point away from it as much as possible to ensure the obstacle doesnt collide with it
             This can obviously be changed to make your own direction of movement when an obstacle is detected.*/
            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[0].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * avoidVelocity);
            //Steer(dirOfMovementToAvoidObstacle);

            Debug.DrawRay(hit2D[0].collider.transform.position, topRight - hit2D[0].collider.transform.position, Color.white);
        }
        else if (hit2D[1])
        {
            //hit2D[1].transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, target.position - this.transform.position));

            dirOfMovementToAvoidObstacle = topLeft - hit2D[1].collider.transform.position;




            dirOfMovementToAvoidObstacle *= Vector2.Distance(transform.position, hit2D[1].collider.transform.position);
            rb.AddForce(dirOfMovementToAvoidObstacle * avoidVelocity);
            //Steer(dirOfMovementToAvoidObstacle);

            Debug.DrawRay(hit2D[1].collider.transform.position, topLeft - hit2D[1].collider.transform.position, Color.white);
        }
        /* If no obstacle was detected, then just steer it towards it's current velocity */
        else
        {
            vector2 = target.transform.position - this.transform.position;
            rb.AddForce(vector2.normalized * velocity);//Steer(location + (velocity.normalized * velocity.magnitude));
        }
    }



}
