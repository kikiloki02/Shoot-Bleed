using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Update()
    {
        if (!_gotHit)
        {
            _sprite1Color = _spriteRenderer.color;
            _sprite2Color = _spriteRenderer2.color;
            _sprite3Color = _spriteRenderer3.color;
        }

        if (AmIDead())
        {
            Debug.Log("Bat->Dead");

            _player.GetComponent<Player_Movement_Script>().Heal(5);

            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Y)) { GetHit(1); }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack) { StartCoroutine(Charging(_chargeTime)); }
    }

// ------ METHODS: ------

    void Attack1()
    {
        Debug.Log("Bat->Attack1");

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        StartCoroutine(AttackCooldown(_cooldownTime));
    }

    void GetHit(int value)
    {
        _gotHit = true;

        _healthValue -= value;

        StartCoroutine(GetHitEffect());
    }

    bool AmIDead()
    {
        return _healthValue <= 0;
    }

// ------ COROUTINES: ------

    IEnumerator Charging(float seconds)
    {
        Debug.Log("Bat->Charging");

        _canAttack = false;

        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _spriteRenderer.color = new Color(255, 0, 0);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Bat->Finished charging");

        _spriteRenderer.color = new Color(255, 255, 255);

        Attack1();
    }

    IEnumerator AttackCooldown(float seconds)
    {
        Debug.Log("Bat->Cooldown started");

        _spriteRenderer.color = new Color(0, 0, 255);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Bat->Cooldown finished");

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;
    }

    IEnumerator GetHitEffect()
    {
        _spriteRenderer.color = new Color(0, 255, 0);
        _spriteRenderer2.color = new Color(0, 255, 0);
        _spriteRenderer3.color = new Color(0, 255, 0);

        yield return new WaitForSeconds(_hitEffectDuration);

        _spriteRenderer.color = _sprite1Color;
        _spriteRenderer2.color = _sprite2Color;
        _spriteRenderer3.color = _sprite3Color;

        _gotHit = false;
    }
}

