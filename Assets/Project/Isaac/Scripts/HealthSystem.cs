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

    public GameObject explosionRadius;

    private Player_Controller player_Controller;

    // Start is called before the first frame update
    void Start()
    {
        player_Controller = FindObjectOfType<Player_Controller>();
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
        if (currentHealth <= 0)
        {
            if (this.gameObject.CompareTag("Enemy"))
            {
                GameObject Healer;
                //FindObjectOfType<PlayerLifeManagement>().RecoverHealth(enemy.GetComponent<Enemy>()._healPlayer);

                Healer = Instantiate(healer, this.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                FindObjectOfType<ManageRoom>().totalEnemies--;
                RewardSystem rewdSys = FindObjectOfType<RewardSystem>();
                rewdSys.enemiesKilled++;
                rewdSys.AddCombo();

                Healer.GetComponent<Healer>().SetPointsToHeal(_healPlayer);
            }
            for (int i = 0; i < player_Controller._playerUpgrades.Count; i++)
            {
                if (player_Controller._playerUpgrades[i]._id == 2 && player_Controller._playerUpgrades[i]._active == true)
                {
                    Instantiate(explosionRadius,transform.position,Quaternion.Euler(0,0,0));
                }
            }

            Destroy(this.gameObject); // <-- AQUÍ
        }
    }
}
