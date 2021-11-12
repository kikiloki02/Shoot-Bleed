using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour // Change to Enemy
{
    public int _healthValue;
    public int _attackValue;
    public int _movementSpeed;

    public Rigidbody2D _rigidBody;
    public Collider2D _attackDetectionZone;
    public SpriteRenderer _spriteRenderer;

    private bool _canAttack = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack) { StartCoroutine(Charging(2.0f)); }
    }

    void Attack1()
    {
        Debug.Log("Bat->Attack1");

        _rigidBody.AddForce(Vector2.right * 350f);

        if (_rigidBody.velocity.magnitude > 1f)
        {
            float maxSpeed = Mathf.Lerp(_rigidBody.velocity.magnitude, 1f, Time.fixedDeltaTime * 5f);
            _rigidBody.velocity = (_rigidBody.velocity.normalized * maxSpeed) / 2;
        }

        StartCoroutine(AttackCooldown(5.0f));
    }

    IEnumerator AttackCooldown(float seconds)
    {
        Debug.Log("Bat->Cooldown started");

        _canAttack = false;

        _spriteRenderer.color = new Color(0, 0, 255);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Bat->Cooldown finished");

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;
    }

    IEnumerator Charging(float seconds)
    {
        Debug.Log("Bat->Charging");

        _spriteRenderer.color = new Color(255, 0, 0);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Bat->Finished charging");

        _spriteRenderer.color = new Color(255, 255, 255);

        Attack1();
    }
}

