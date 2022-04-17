using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hab3 : Upgrades // fire Rate
{
    
    float percentage;
    Player_Controller player_Controller;
    public GameObject _weapon;
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
        player_Controller = FindObjectOfType<Player_Controller>();

        player_Controller._weapon.GetComponent<ShootBullet>().secondsPerBullet -= 0.08f;
        //player_Controller._secondsPerBullet *= (0.5f);
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
