using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    public virtual void GetDamage(int damage)
    {
        currentHealth -= damage;

        if(enemy!= null)
        enemy.GetComponent<Enemy>().GetHit();
    }

    void Death()
    {
        if(currentHealth <= 0) 
        {
            if (this.gameObject.CompareTag("Enemy"))
            {
                FindObjectOfType<PlayerLifeManagement>().RecoverHealth(enemy.GetComponent<Enemy>()._healPlayer);
            }
            Destroy(this.gameObject); 
        }
    }
}
