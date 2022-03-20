using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Enemy
{
// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start() // DONE!
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;

        StartCoroutine(StartFuse(_attack1ChargeTime));
    }

    private void Update()
    {
        
    }

    // ------ METHODS: ------

    void Explode()
    {
        // GetComponent<Animator>().SetTrigger("Explode");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attack2.Play(); // Attack2 SFX

        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack1));
    }

    // ------ COROUTINES: ------

    public IEnumerator StartFuse(float time) // DONE!
    {
        // GetComponent<Animator>().SetTrigger("StartFuse");

        _charge.Play();

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        Explode();
    }

    IEnumerator AttackColliderSwitch(int attack, float secondsActive) // DONE!
    {
        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[attack].gameObject.SetActive(true);

        yield return new WaitForSeconds(secondsActive);

        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[attack].gameObject.SetActive(false);

        Destroy(this.gameObject);
    }
}

