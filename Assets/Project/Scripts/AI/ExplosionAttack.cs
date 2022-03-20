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

        StartCoroutine(Activate());
        //_player = FindObjectOfType<Player_Controller>().gameObject;
    }
    public IEnumerator Activate()
    {
        this.transform.parent = null;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;

        yield return new WaitForSeconds(explosionActiveTime);

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
                //_player.GetComponent<PlayerLifeManagement>().GetDamage(_enemy.GetComponent<Enemy>()._attackValue);
            }
        }
    }
}
