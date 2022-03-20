using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialHeal : MonoBehaviour
{

    private PlayerLifeManagement playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerLifeManagement>();

        playerHealth.currentHealth = playerHealth.maxHealth;
        FindObjectOfType<HealthBar>().SetHealth(playerHealth.currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
