using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab4 : Upgrades
{
    
    // Start is called before the first frame update
    void Start()
    {
        price = 150;
        idx = 4;
        isActive = false; 
    }
    public override void Activate()
    {
        // aqui va el efecte de la hab??

        base.Activate();
    }
    public override void Disactivate()
    {
        base.Disactivate();
    }
}
