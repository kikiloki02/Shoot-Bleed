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

            _audioManager.PlaySFX(3); // Die

            _player.GetComponent<Player_Movement_Script>().Heal(5);

            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Y)) { GetHit(1); }
        if (Input.GetKeyDown(KeyCode.T)) { _audioManager.PlaySFX(0); } // Should be: AudioManager.instance.PlaySFX(value);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack) { StartCoroutine(Charging(_chargeTime)); }
    }

// ------ METHODS: ------

    void Attack1()
    {
        Debug.Log("Bat->Attack1");

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack

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

    IEnumerator Attack2()
    {
        Debug.Log("Bat->Attack2");

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack

        Vector3 _storedPosition = this.transform.position;

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        yield return new WaitForSeconds(0.5f);

        _chargeDirection = _storedPosition - this.transform.position;
        _chargeDirection.Normalize();

        _attackParticles.Play();

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        StartCoroutine(AttackCooldown(_cooldownTime));
    }

    IEnumerator Attack3()
    {
        Debug.Log("Bat->Attack3");

        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _attackParticles.Play();

        _audioManager.PlaySFX(1); // Attack

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        yield return new WaitForSeconds(0.5f);

        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _attackParticles.Play();

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        StartCoroutine(AttackCooldown(_cooldownTime));
    }

    IEnumerator Charging(float seconds)
    {
        Debug.Log("Bat->Charging");

        int _randomNumber = Random.Range(0, 7); // min included, max excluded

        switch (_randomNumber)
        {
            case 0:
            case 1:
            case 2:
                _audioManager.PlaySFX(2); // Charge
                _chargingParticlesBasic.Play();
                break;
            case 3:
            case 4:
                _audioManager.PlaySFX(2); // Charge
                _chargingParticlesForthAndBack.Play();
                break;
            case 5:
            case 6:
                _audioManager.PlaySFX(2); // Charge
                _chargingParticlesChain.Play();
                break;
        }

        _canAttack = false;

        _chargeDirection = _player.GetComponent<Transform>().position - this.transform.position;
        _chargeDirection.Normalize();

        _spriteRenderer.color = new Color(255, 0, 0);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Bat->Finished charging");

        _spriteRenderer.color = new Color(255, 255, 255);

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
                StartCoroutine(Attack2());
                break;
            case 5:
            case 6:
                _chargingParticlesChain.Stop();
                StartCoroutine(Attack3());
                break;
        }
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
        _audioManager.PlaySFX(3); // Hit

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

