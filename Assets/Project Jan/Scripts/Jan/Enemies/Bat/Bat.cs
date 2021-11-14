using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public ParticleSystem _chargingParticlesBasic;
    public ParticleSystem _chargingParticlesForthAndBack;
    public ParticleSystem _chargingParticlesChain;
    public ParticleSystem _attackParticles;

    public AudioManager _audioManager;

    public GameObject _attackCollider;
    // TODO Change this for a Collider2D and find how to execute OnTriggerStay2D() with this specific collider.

    // TODO Modify the Methods so that they are more compatible with AttackColliderSwitch coroutine.

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
            Die();

            HealPlayer();
        }

        // Tests:
        if (Input.GetKeyDown(KeyCode.Y)) { GetHit(1); }
        if (Input.GetKeyDown(KeyCode.T)) { _audioManager.PlaySFX(0); } // Should be: AudioManager.instance.PlaySFX(value);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack) { StartCoroutine(Charging(_chargeTime)); }
    }

// ------ METHODS: ------

    public override void Die()
    {
        Debug.Log("Bat->Dead");

        _audioManager.PlaySFX(3); // Die SFX

        Destroy(this.gameObject);
    }

    public override void HealPlayer()
    {
        _player.GetComponent<Player_Controller>().Heal(5);
    }

    public override void GetHit(int value)
    {
        _gotHit = true;

        _healthValue -= value;

        StartCoroutine(GetHitEffect());
    }

    public override bool AmIDead()
    {
        return _healthValue <= 0;
    }

    void Attack1()
    {
        Debug.Log("Bat->Attack1");

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack1 SFX

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance); // The attack move
        StartCoroutine(AttackColliderSwitch(1f));

        _attackCollider.transform.right = _rigidBody.velocity;

        StartCoroutine(AttackCooldown(_cooldownTime));
    }

// ------ COROUTINES: ------

    public override IEnumerator GetHitEffect()
    {
        _audioManager.PlaySFX(3); // Hit SFX

        _spriteRenderer.color = new Color(0, 255, 0);
        _spriteRenderer2.color = new Color(0, 255, 0);
        _spriteRenderer3.color = new Color(0, 255, 0);

        yield return new WaitForSeconds(_hitEffectDuration); // Wait

        _spriteRenderer.color = _sprite1Color;
        _spriteRenderer2.color = _sprite2Color;
        _spriteRenderer3.color = _sprite3Color;

        _gotHit = false;
    }

    IEnumerator Attack2(float seconds)
    {
        Debug.Log("Bat->Attack2");

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack1 SFX

        // The Attack move:
        Vector3 _storedPosition = this.transform.position;

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);
        StartCoroutine(AttackColliderSwitch(1f));

        yield return new WaitForSeconds(seconds); // Wait

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack1 SFX

        // The Attack move 2:
        _chargeDirection = _storedPosition - this.transform.position;
        _chargeDirection.Normalize();

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);
        StartCoroutine(AttackColliderSwitch(1f));
        // ------

        StartCoroutine(AttackCooldown(_cooldownTime));
    }

    IEnumerator Attack3(float seconds)
    {
        Debug.Log("Bat->Attack3");

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack1 SFX

        // The Attack move:
        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);
        StartCoroutine(AttackColliderSwitch(1f));

        yield return new WaitForSeconds(seconds); // Wait

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack1 SFX

        // The Attack move 2:
        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);
        StartCoroutine(AttackColliderSwitch(1f));
        // ------

        StartCoroutine(AttackCooldown(_cooldownTime));
    }

    IEnumerator Charging(float seconds)
    {
        Debug.Log("Bat->Charging");

        // Random attack move: (between 3 attacks)
        int _randomNumber = Random.Range(0, 7); // min included, max excluded

        // Show the according particles and play the according sound to telegraph the attack:
        switch (_randomNumber)
        {
            case 0:
            case 1:
            case 2:
                _audioManager.PlaySFX(2); // Charge1 SFX
                _chargingParticlesBasic.Play();
                break;
            case 3:
            case 4:
                _audioManager.PlaySFX(2); // Charge1 SFX
                _chargingParticlesForthAndBack.Play();
                break;
            case 5:
            case 6:
                _audioManager.PlaySFX(2); // Charge1 SFX
                _chargingParticlesChain.Play();
                break;
        }

        // Logic:
        _canAttack = false;

        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _spriteRenderer.color = new Color(255, 0, 0);

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("Bat->Finished charging");

        _spriteRenderer.color = new Color(255, 255, 255);

        // Execute the corresponding attack move:
        switch (_randomNumber)
        {
            case 0:
            case 1:
            case 2:
                _chargingParticlesBasic.Stop();
                Attack1();
                break;
            case 3:
            case 4:
                _chargingParticlesForthAndBack.Stop();
                StartCoroutine(Attack2(0.5f));
                break;
            case 5:
            case 6:
                _chargingParticlesChain.Stop();
                StartCoroutine(Attack3(0.5f));
                break;
        }
    }

    IEnumerator AttackCooldown(float seconds)
    {
        Debug.Log("Bat->Cooldown started");

        _spriteRenderer.color = new Color(0, 0, 255);

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("Bat->Cooldown finished");

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;
    }

    IEnumerator AttackColliderSwitch(float secondsActive)
    {
        _attackCollider.SetActive(true);

        yield return new WaitForSeconds(secondsActive);

        _attackCollider.SetActive(false);
    }
}

