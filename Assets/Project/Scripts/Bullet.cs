using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Restar vida al enemy

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
