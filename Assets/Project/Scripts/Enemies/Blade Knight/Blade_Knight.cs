using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_Knight : Enemy
{
// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;

        _spawnedSound.Play();
        _spawnedParticles.Play();

        _spriteRedColor = new Color32(255, 100, 100, 255);
        _spriteBlueColor = new Color32(255, 255, 255, 255); // Now it's white as well
        _spriteWhiteColor = new Color32(255, 255, 255, 255);
    }

    private void Update()
    {
        // Flip the SpriteRederer (X axis) when switching from moving left to right and vice-versa:

        //bool _spriteRendererFlipXValue = GetComponent<SpriteRenderer>().flipX;

        //if (_movement.x < -0.01f && !_spriteRendererFlipXValue)
        //{
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}
        //else if (_movement.x > 0.01f && _spriteRendererFlipXValue)
        //{
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Random attack move: (between 3 attacks)
        if (doRandom)
        {
            doRandom = false;
            _randomNumber = Random.Range(0, 7); // min included, max excluded
        }

        if (collision.gameObject.tag == "Player" && _canAttack)
        {
            switch (_randomNumber)
            {
                case 0:
                case 1:
                case 2:
                    StartCoroutine(Charging(_attack1ChargeTime));
                    break;
                case 3:
                case 4:
                    StartCoroutine(Charging(_attack2ChargeTime));
                    break;
                case 5:
                case 6:
                    StartCoroutine(Charging(_attack3ChargeTime));
                    break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().GetDamage(_collisionDamageValue);
        }
    }

    // ------ METHODS: ------

    public override void Die()
    {
        AudioSource.PlayClipAtPoint(_death.clip, Camera.main.transform.position, 0.2f);

        // Death particles:
    }

    public override void GetHit()
    {
        _gotHit = true;

        StartCoroutine(GetHitEffect());
    }

    void Attack1()
    {
        GetComponent<Animator>().SetTrigger("Attack");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack1));
        // ------

        StartCoroutine(AttackCooldown(_attack1Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    void Attack2(float seconds)
    {
        GetComponent<Animator>().SetTrigger("Attack");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[1].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        StartCoroutine(AttackColliderSwitch(1, _activeTimeAttack1));
        // ------

        StartCoroutine(AttackCooldown(_attack2Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    void Attack3(float seconds)
    {
        GetComponent<Animator>().SetTrigger("Attack");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[2].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack2.Play(); // Attack2 SFX

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        StartCoroutine(AttackColliderSwitch(2, _activeTimeAttack1));
        // ------

        StartCoroutine(AttackCooldown(_attack3Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

// ------ COROUTINES: ------

    public override IEnumerator GetHitEffect()
    {
        _hit.Play(); // Hit SFX

        _spriteRenderer.color = _spriteRedColor;

        yield return new WaitForSeconds(_hitEffectDuration); // Wait

        if (_isCharging)
        {
            _spriteRenderer.color = new Color32(255, 0, 0, 255);
        }
        else if (_cooldown)
        {
            _spriteRenderer.color = _spriteBlueColor;
        }
        else
        {
            _spriteRenderer.color = _spriteWhiteColor;
        }

        _gotHit = false;
    }

    IEnumerator Charging(float seconds)
    {
        GetComponent<Animator>().SetTrigger("Charge");

        _isCharging = true;

        this.GetComponent<Seek>().enabled = false;

        _chargeDirection = _player.GetComponent<Transform>().position - this.gameObject.transform.position;
        _chargeDirection.Normalize();

        _attackIndicator.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        // Random attack move: (between 3 attacks)
        if(doRandom)
            _randomNumber = Random.Range(0, 7); // min included, max excluded

        // Show the according particles and play the according sound to telegraph the attack:
        switch (_randomNumber)
        {
            case 0:
            case 1:
            case 2:
                _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(true);
                _charge.Play(); // Charge1 SFX
                _chargingParticlesBasic.Play();
                break;
            case 3:
            case 4:
                _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[1].gameObject.SetActive(true);
                _charge.Play(); // Charge1 SFX
                _chargingParticlesForthAndBack.Play();
                break;
            case 5:
            case 6:
                _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[2].gameObject.SetActive(true);
                _charge.Play(); // Charge1 SFX
                _chargingParticlesChain.Play();
                break;
        }

        // Logic:
        _canAttack = false;

        _spriteRenderer.color = new Color(255, 0, 0);

        yield return new WaitForSeconds(seconds); // Wait

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
                Attack2(0.5f);
                break;
            case 5:
            case 6:
                _chargingParticlesChain.Stop();
                Attack3(0.5f);
                break;
        }

        _isCharging = false;
    }

    IEnumerator AttackCooldown(float seconds)
    {
        _cooldown = true;

        _spriteRenderer.color = _spriteBlueColor;

        yield return new WaitForSeconds(seconds); // Wait

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;

        _cooldown = false;
    }

    IEnumerator AttackColliderSwitch(int attack, float secondsActive)
    {
        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[attack].gameObject.SetActive(true);

        yield return new WaitForSeconds(secondsActive);

        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[attack].gameObject.SetActive(false);
    }
}

