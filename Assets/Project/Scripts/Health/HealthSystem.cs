using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private bool dead = false;

    public int _healPlayer;

    public GameObject healer;

    public GameObject enemy;

    public GameObject explosionRadius;

    private Player_Controller player_Controller;

    public GameObject _blood;

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
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            if (this.gameObject.CompareTag("Enemy"))
            {
                FindObjectOfType<ManageRoom>().totalEnemies--;
                RewardSystem rewdSys = FindObjectOfType<RewardSystem>();
                rewdSys.enemiesKilled++;
                rewdSys.AddCombo();
            }
            for (int i = 0; i < player_Controller._playerUpgrades.Count; i++)
            {
                if (player_Controller._playerUpgrades[i]._id == 2 && player_Controller._playerUpgrades[i]._active == true)
                {
                    Instantiate(explosionRadius,transform.position,Quaternion.Euler(0,0,0));
                }
            }

            GetComponent<Animator>().SetBool("Dead", true);
            
        }
    }

    public void SpawnHealer()
    {
        GameObject Healer;

        Healer = Instantiate(healer, this.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Healer.GetComponent<Healer>().SetPointsToHeal(_healPlayer);
    }

    public void DesactivateBehaviours()
    {
        GetComponent<Enemy>()._attackIndicator.SetActive(false); ;
        GetComponent<Enemy>().enabled = false;
        GetComponent<Seek>().enabled = false;
        if(GetComponent<CapsuleCollider2D>() != null) { GetComponent<CapsuleCollider2D>().enabled = false; }
        else { GetComponent<CircleCollider2D>().enabled = false; }
        GetComponent<CircleCollider2D>().enabled = false;
    }
    public void DestroyAfterDeath()
    {
       Instantiate(_blood, this.gameObject.transform.position, Quaternion.Euler(0, 0, 0));

        Destroy(this.gameObject);
    }
}
