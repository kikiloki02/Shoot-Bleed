using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab2 : Upgrades //movement Speed
{
    private float movementSpeed;
    public float percentage; // porcentage en el inspector
                             // Start is called before the first frame update
    void Start()
    {
        base.Start();
        isActive = false;

        movementSpeed = FindObjectOfType<Player_Controller>()._movementSpeed;
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
        movementSpeed = (int) (movementSpeed * (1 + percentage)); 
       
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
