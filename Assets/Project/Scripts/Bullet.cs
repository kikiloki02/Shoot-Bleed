using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;
    public Rigidbody2D rb;
    public float OutVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(this.transform.right * OutVelocity, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Restar vida al enemy
            other.gameObject.GetComponent<HealthSystem>().GetDamage(damage);
            //Knockback al enemy
            //other.rigidbody.AddForce(force * 0.1f);
            //Destriur bala
            Destroy(this.gameObject);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            //Destruir bala
            Destroy(this);
        }
    }
}
