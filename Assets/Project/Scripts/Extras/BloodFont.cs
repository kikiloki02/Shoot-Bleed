using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFont : MonoBehaviour
{

    public float secondsBetweenHeal;
    public bool isInLobby;

    private PlayerLifeManagement playerLife;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLifeManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Heal(1, other.gameObject));
            if (isInLobby) { playerLife.RecoverHealth(playerLife.maxHealth); }
            else { playerLife.RecoverHealth(playerLife.maxHealth/2); }
            playerLife.healthBar.SetHealth(playerLife.currentHealth);
            
        }
    }

    IEnumerator Heal(float secondsBetweenHeal, GameObject player)
    {
        player.GetComponent<PlayerLifeManagement>().RecoverHealth(10);
        player.GetComponent<PlayerLifeManagement>().healthBar.SetHealth(player.GetComponent<PlayerLifeManagement>().currentHealth);
        yield return new WaitForSeconds(secondsBetweenHeal);
    }
}
