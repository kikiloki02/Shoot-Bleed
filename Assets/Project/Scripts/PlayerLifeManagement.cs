using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManagement : HealthSystem
{

    public bool criticalState = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        criticalState = isCritical();
    }

    public void LoseLife()
    {
        currentHealth -= 1;
    }

    bool isCritical()
    {
        return (currentHealth == 0);
    }

    public override void GetDamage(int damage)
    {
        if (criticalState) { Destroy(this.gameObject); }
        else { currentHealth -= damage; }
    }

}
