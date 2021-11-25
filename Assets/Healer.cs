using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{

    public GameObject player;
    public int pointsToHeal;
    public float velocity;
    public float maxVelocity;
    public float distanceToFollow;
    public bool followPlayer;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLifeManagement>().gameObject;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerLifeManagement>().RecoverHealth(pointsToHeal);
            Destroy(this.gameObject);
        }
    }

    private void FollowPlayer()
    {
        Transform playerTransform = player.GetComponent<Transform>();
        Vector2 thisPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        float playerToHealerDistance = Vector2.Distance(thisPosition, playerPosition);
        if (playerToHealerDistance < distanceToFollow && followPlayer == false)
        {
            followPlayer = true;
        }

        if (followPlayer) 
        {
            Vector2 playerToHealerVector2 = playerPosition - thisPosition;
            playerToHealerVector2 = playerToHealerVector2.normalized;
            velocity = velocity / playerToHealerDistance;
           
            if(velocity > maxVelocity)
            {
                velocity = maxVelocity;
            }
            else if(velocity < 2)
            {
                velocity = 2;
            }

            this.gameObject.GetComponent<Rigidbody2D>().AddForce(playerToHealerVector2 * velocity);
        }
    }
}
