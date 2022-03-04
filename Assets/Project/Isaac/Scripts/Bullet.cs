using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public float OutVelocity;
    public ParticleSystem particles;
    public TrailParticles trailParticles;

    // Start is called before the first frame update
    void Start()
    {
        particles.Pause();
        rb.AddForce(this.transform.right * OutVelocity, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            particles.Play();
            //Restar vida al enemy
            other.gameObject.GetComponent<HealthSystem>().GetDamage(damage);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            //Destruir bala
            trailParticles.DetachAndDestroy();
            Destroy(this.gameObject);
        }
    }
}
