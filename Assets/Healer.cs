using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public int pointsToHeal;
    public float velocity;
    public float maxVelocity;
    public float distanceToFollow;
    public bool followPlayer;

    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = this.gameObject.GetComponent<ParticleSystem>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLifeManagement>().RecoverHealth(pointsToHeal);

            Destroy(gameObject);
        }
    }
}
