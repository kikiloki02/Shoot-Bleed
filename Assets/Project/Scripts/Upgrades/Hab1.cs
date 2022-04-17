using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab1 : Upgrades // Max Health
{
    private int maxHealth;
    // Start is called before the first frame update
    void Awake()
    {
        base.Start();
        isActive = false;

        maxHealth = FindObjectOfType<HealthSystem>().maxHealth;
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
        maxHealth += 5;
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
