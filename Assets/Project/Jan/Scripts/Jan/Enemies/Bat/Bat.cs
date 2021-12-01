using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;

        _spriteRedColor = new Color32(255, 100, 100, 255);
        _spriteBlueColor = new Color32(0, 0, 255, 255);
        _spriteWhiteColor = new Color32(255, 255, 255, 255);
    }

    private void Update()
    {
        // Tests:
        //if (Input.GetKeyDown(KeyCode.Y)) { GetHit(); }
        //if (Input.GetKeyDown(KeyCode.R)) { _attack1.Play(); ; } // Should be: AudioManager.instance.PlaySFX(value);

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    doRandom = true;
        //}

        //if (Input.GetKeyDown(KeyCode.X)) 
        //{
        //    doRandom = false;
        //    _randomNumber = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    doRandom = false;
        //    _randomNumber = 3;
        //}

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    doRandom = false;
        //    _randomNumber = 5;
        //}

        RotateIndicator();
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

// ------ METHODS: ------

    public override void Die()
    {
        Debug.Log("Bat->Dead");

        _death.Play(); // Die SFX
    }

    public override void GetHit()
    {
        _gotHit = true;

        StartCoroutine(GetHitEffect());
    }

    void Attack1()
    {
        Debug.Log("Bat->Attack1");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance); // The attack move
        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack1));

        StartCoroutine(AttackCooldown(_attack1Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    void RotateIndicator()
    {
        if (_rotate)
        {
            Vector2 _newDirection;

            _newDirection = _player.transform.position - this.gameObject.transform.position;
            _newDirection.Normalize();

            _attackIndicator.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _newDirection));
        }
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

    IEnumerator Attack2(float seconds)
    {
        Debug.Log("Bat->Attack2");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // The Attack move:
        Vector3 _storedPosition = this.gameObject.transform.position;

        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);
        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack2Forth));

        yield return new WaitForSeconds(seconds); // Wait

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // The Attack move 2:
        _chargeDirection = _storedPosition - this.gameObject.transform.position;
        _chargeDirection.Normalize();

        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);
        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack2Back));
        // ------

        StartCoroutine(AttackCooldown(_attack2Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    IEnumerator Attack3(float seconds)
    {
        Debug.Log("Bat->Attack3");

        _rotate = false;
        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[1].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack2.Play(); // Attack2 SFX

        // The Attack move:
        _chargeDirection = _player.GetComponent<Transform>().position - this.gameObject.transform.position;
        _chargeDirection.Normalize();

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack3First));

        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        yield return new WaitForSeconds(seconds); // Wait

        _attackParticles.Play();

        _attack2.Play(); // Attack2 SFX

        // The Attack move 2:
        _chargeDirection = _player.GetComponent<Transform>().position - this.gameObject.transform.position;
        _chargeDirection.Normalize();

        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        _rigidBody.AddForce(_movementSpeed * _chargeDirection * _chargeDistance);

        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack3Second));
        // ------

        StartCoroutine(AttackCooldown(_attack3Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    IEnumerator Charging(float seconds)
    {
        Debug.Log("Bat->Charging");

        _isCharging = true;

        this.GetComponent<Seek>().enabled = false;

        _chargeDirection = _player.GetComponent<Transform>().position - this.gameObject.transform.position;
        _chargeDirection.Normalize();

        _attackIndicator.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

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
                _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(true);
                _charge.Play(); // Charge1 SFX
                _chargingParticlesForthAndBack.Play();
                break;
            case 5:
            case 6:
                _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[1].gameObject.SetActive(true);
                _charge.Play(); // Charge1 SFX
                _chargingParticlesChain.Play();
                _rotate = true;
                break;
        }

        // Logic:
        _canAttack = false;

        _spriteRenderer.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("Bat->Finished charging");

        _spriteRenderer.color = _spriteWhiteColor;

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
                StartCoroutine(Attack2(_attack2point5Cooldown));
                break;
            case 5:
            case 6:
                _chargingParticlesChain.Stop();
                StartCoroutine(Attack3(_attack3point5Cooldown));
                break;
        }

        _isCharging = false;
    }

    IEnumerator AttackCooldown(float seconds)
    {
        Debug.Log("Bat->Cooldown started");

        _cooldown = true;

        _spriteRenderer.color = _spriteBlueColor;

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("Bat->Cooldown finished");

        _spriteRenderer.color = _spriteWhiteColor;

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

