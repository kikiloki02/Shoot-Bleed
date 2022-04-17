using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hab1 : Upgrades // Max Health
{
    
    private Player_Controller player_Controller;
    // Start is called before the first frame update

    void Start()
    {
        base.Start();
        isActive = false;
        shopUpgrade = FindObjectOfType<ShopUpgrade>();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    public override void Activate()
    {
        base.Activate();
        //accion de la habilidad
        player_Controller = FindObjectOfType<Player_Controller>();

        player_Controller.GetComponent<PlayerLifeManagement>().maxHealth += 5;
        player_Controller.GetComponent<PlayerLifeManagement>().currentHealth += 5;

        player_Controller.GetComponent<PlayerLifeManagement>().healthBar.GetComponent<HealthBar>().SetMaxHealth(player_Controller.GetComponent<PlayerLifeManagement>().maxHealth);
        player_Controller.GetComponent<PlayerLifeManagement>().healthBar.GetComponent<HealthBar>().SetHealth(player_Controller.GetComponent<PlayerLifeManagement>().currentHealth);
        
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void Disactivate()
    {
        base.Disactivate();
    }

    public override int GetIndex()
    {
        return idx;
    }

    public override IEnumerator ShowPickedUpText(float seconds)
    {
        return base.ShowPickedUpText(seconds);
    }

}
