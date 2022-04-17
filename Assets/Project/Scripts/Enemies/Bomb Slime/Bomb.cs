using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Enemy
{
// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start() // DONE!
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;
    }

    private void Update()
    {
        
    }

    // ------ METHODS: ------

    public void Explode()
    {
        // GetComponent<Animator>().SetTrigger("Explode");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attack2.Play(); // Attack2 SFX
    }

    // ------ COROUTINES: ------

    public void StartFuse(float time) // DONE!
    {
        _charge.Play();
    }

    public void AttackColliderSwitchPhase1() // DONE!
    {
        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(true);
    }

    public void AttackColliderSwitchPhase2() // DONE!
    {
        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        Destroy(this.gameObject);
    }
}

