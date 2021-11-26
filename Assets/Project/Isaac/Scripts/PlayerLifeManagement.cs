using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManagement : HealthSystem
{

    public bool criticalState = false;
    public HealthBar healthBar;
    float currentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Mathf.Min(0.75f, currentTime + Time.unscaledDeltaTime);
        //Time.timeScale = Mathf.Sin((currentTime / 0.5f) * Mathf.PI * 0.5f);
        Time.timeScale = Mathf.Pow((currentTime / 0.75f), 2f);

        criticalState = isCritical();
    }

    public void LoseLife()
    {
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
    }

    bool isCritical()
    {
        return (currentHealth == 0);
    }

    public override void GetDamage(int damage)
    {
        currentTime = 0f;

        if (criticalState) { Destroy(this.gameObject); }
        else 
        { 
            currentHealth -= damage;
            if (currentHealth < 0)
                currentHealth = 0;
            healthBar.SetHealth(currentHealth);
            CinemachineShake.Instance.ShakeCamera(5f, 0.15f);
        }
    }

    public void RecoverHealth(int addHp)
    {
        currentHealth += addHp;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
