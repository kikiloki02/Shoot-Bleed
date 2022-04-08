using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab1 : Upgrades
{
    // Start is called before the first frame update
    void Start()
    {
        price = 50;
        idx = 1;
        isActive = false;
    }

   public override void Activate()
    {
        base.Activate();
    }

    public override void Disactivate()
    {
        base.Disactivate();
    }

}
