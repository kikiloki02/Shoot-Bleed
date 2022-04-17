using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour
{

    public float explosionActiveTime;
    public int explosionDamage;
    private bool exploded;

    // private bool _canDealDamage = true;

    // ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        exploded = false;
    }
    public void Activate()
    {
        this.transform.parent = null;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void DestroyAfterDeath()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!exploded)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<HealthSystem>().GetDamage(explosionDamage);
                exploded = true;
            }
        }
    }
}
