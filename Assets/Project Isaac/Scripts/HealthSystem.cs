using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

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
    }

    void Death()
    {
        if(currentHealth <= 0) { Destroy(this.gameObject); }
    }
}
