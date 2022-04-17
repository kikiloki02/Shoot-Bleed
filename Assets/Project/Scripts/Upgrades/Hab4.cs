using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab4 : Upgrades //
{
    public float percentage;

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
        if(shopUpgrade == null){
            shopUpgrade = FindObjectOfType<ShopUpgrade>();
        }
        for (int i = 0; i < shopUpgrade.exposedUpgrades.Count; i++)
        {
            int newPrice = (int)(shopUpgrade.GetComponent<ShopUpgrade>().exposedUpgrades[i].GetComponent<Upgrades>().price * (1 - percentage));
            //shopUpgrade.GetComponent<ShopUpgrade>().exposedUpgrades[i].GetComponent<Upgrades>().price = newPrice;
            shopUpgrade.GetComponent<ShopUpgrade>().exposedUpgrades[i].GetComponent<Upgrades>().ChangeTextPrice(newPrice);
        }
        for (int i = 0; i < shopUpgrade.upgradesInShop.Count; i++)
        {
            int newPrice = (int)(shopUpgrade.GetComponent<ShopUpgrade>().upgradesInShop[i].GetComponent<Upgrades>().price * (1 - percentage));
            //shopUpgrade.GetComponent<ShopUpgrade>().upgradesInShop[i].GetComponent<Upgrades>().price = newPrice;
            shopUpgrade.GetComponent<ShopUpgrade>().upgradesInShop[i].GetComponent<Upgrades>().ChangeTextPrice(newPrice);
        }

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
