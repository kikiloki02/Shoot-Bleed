using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public float OutVelocity;
    public ParticleSystem particles;
    
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
            //Knockback al enemy
            //other.rigidbody.AddForce(force * 0.1f);
            //Destriur bala

            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Shield"))
        {
            //Destruir bala
            Destroy(this.gameObject);
        }
    }

    //void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        particles.Play();
    //        //Restar vida al player
    //        other.gameObject.GetComponent<HealthSystem>().GetDamage(damage);
    //        //Knockback al player
    //        //other.rigidbody.AddForce(force * 0.1f);
    //        //Destriur bala

    //        Destroy(this.gameObject);
    //    }
    //    else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
    //    {
    //        //Destruir bala
    //        Destroy(this.gameObject);
    //    }
    //}
}
