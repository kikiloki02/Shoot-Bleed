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
        //accion de la habilidad PETA x NULL OBJECT
       /*for(int i = 0; i <= shopUpgrade.GetComponent<ShopUpgrade>().upgradesInShop.Count; i++)
       {
            shopUpgrade.GetComponent<ShopUpgrade>().upgradesInShop[i].GetComponent<Upgrades>().price = (int)(shopUpgrade.GetComponent<ShopUpgrade>().upgradesInShop[i].GetComponent<Upgrades>().price * (1 - percentage)); 
       }*/
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
