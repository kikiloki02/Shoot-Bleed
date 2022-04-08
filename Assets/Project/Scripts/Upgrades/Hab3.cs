using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab3 : Upgrades
{
    // Start is called before the first frame update
    void Start()
    {
        price = 100;
        idx = 3;
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
