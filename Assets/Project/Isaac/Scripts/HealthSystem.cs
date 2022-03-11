using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public int _healPlayer;

    public GameObject healer;

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
                GameObject Healer;
                //FindObjectOfType<PlayerLifeManagement>().RecoverHealth(enemy.GetComponent<Enemy>()._healPlayer);

                Healer = Instantiate(healer, this.gameObject.transform.position, Quaternion.Euler(0,0,0));
                FindObjectOfType<ManageRoom>().totalEnemies--;
                RewardSystem rewdSys = FindObjectOfType<RewardSystem>();
                rewdSys.enemiesKilled++;
                rewdSys.AddCombo();

                Healer.GetComponent<Healer>().SetPointsToHeal(_healPlayer);
            }

            Destroy(this.gameObject); // <-- AQUÍ
        }
    }
}
