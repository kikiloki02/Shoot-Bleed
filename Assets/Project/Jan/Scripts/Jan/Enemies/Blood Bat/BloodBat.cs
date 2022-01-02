using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBat : Enemy
{
    public GameObject _bullet;
    public GameObject _smallBullet;

// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;

        _spawnedSound.Play();
        _spawnedParticles.Play();

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

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    doRandom = false;
        //    _randomNumber = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    doRandom = false;
        //    _randomNumber = 3;
        //}

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    doRandom = false;
        //    _randomNumber = 5;
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

// ------ METHODS: ------

    public override void Die()
    {
        Debug.Log("BloodBat->Dead");

        _death.Play(); // Die SFX
    }

    public override void GetHit()
    {
        _gotHit = true;

        StartCoroutine(GetHitEffect());
    }

    void Attack1()
    {
        Debug.Log("BloodBat->Attack1");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // Logic:
        // Instantiate bullet towards _chargeDirection
        GameObject _bulletToShoot;
        _bulletToShoot = Instantiate(_bullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection)));

        _rigidBody.AddForce((_movementSpeed / 3) * -_chargeDirection * _chargeDistance);

        StartCoroutine(AttackCooldown(_attack1Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    void Attack2(float seconds)
    {
        Debug.Log("BloodBat->Attack2");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[1].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack2.Play(); // Attack2 SFX

        // The Attack move:
        GameObject newBullet1;
        GameObject newBullet2;
        GameObject newBullet3;

        newBullet1 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection)));
        newBullet2 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection)));
        newBullet3 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection)));

        newBullet2.transform.rotation = Quaternion.Euler(0, 0, newBullet2.transform.eulerAngles.z + 45);
        newBullet3.transform.rotation = Quaternion.Euler(0, 0, newBullet3.transform.eulerAngles.z - 45);

        _rigidBody.AddForce((_movementSpeed / 3) * -_chargeDirection * _chargeDistance);
        // ------

        StartCoroutine(AttackCooldown(_attack2Cooldown));

        this.GetComponent<Seek>().enabled = true;

        doRandom = true;
    }

    void Attack3(float seconds)
    {
        Debug.Log("BloodBat->Attack3");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[2].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack2.Play(); // Attack2 SFX

        // The Attack move:
        GameObject newBullet1;
        GameObject newBullet2;
        GameObject newBullet3;
        GameObject newBullet4;
        GameObject newBullet5;
        GameObject newBullet6;
        GameObject newBullet7;
        GameObject newBullet8;

        newBullet1 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.up)));
        newBullet2 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.down)));
        newBullet3 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.left)));
        newBullet4 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.right)));
        newBullet5 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.one, Vector2.up)));
        newBullet6 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.one, Vector2.down)));
        newBullet7 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.one, Vector2.left)));
        newBullet8 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.one, Vector2.right)));
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
        Debug.Log("BloodBat->Charging");

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

        Debug.Log("BloodBat->Finished charging");

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
        Debug.Log("BloodBat->Cooldown started");

        _cooldown = true;

        _spriteRenderer.color = new Color(0, 0, 255);

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("BloodBat->Cooldown finished");

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;

        _cooldown = false;
    }
}

