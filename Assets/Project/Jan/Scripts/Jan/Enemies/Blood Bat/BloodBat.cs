using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBat : Enemy
{
    public ParticleSystem _chargingParticlesBasic;
    public ParticleSystem _chargingParticlesForthAndBack;
    public ParticleSystem _chargingParticlesChain;
    public ParticleSystem _attackParticles;

    //public AudioManager _audioManager;

    public GameObject _attackIndicator;
    public GameObject _bullet;
    public GameObject _smallBullet;

    public AudioSource _attack1;
    public AudioSource _attack2;
    public AudioSource _charge;
    public AudioSource _hit;
    public AudioSource _death;

    // TODO Modify the Methods so that they are more compatible with AttackColliderSwitch coroutine.

    // ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;
    }

    private void Update()
    {
        if (!_gotHit)
        {
            _sprite1Color = _spriteRenderer.color;
            _sprite2Color = _spriteRenderer2.color;
            _sprite3Color = _spriteRenderer3.color;
        }

        // Tests:
        if (Input.GetKeyDown(KeyCode.Y)) { GetHit(); }
        if (Input.GetKeyDown(KeyCode.R)) { _attack1.Play(); ; } // Should be: AudioManager.instance.PlaySFX(value);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack) { StartCoroutine(Charging(_chargeTime)); }
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

        StartCoroutine(AttackCooldown(_cooldownTime));

        this.GetComponent<Seek>().enabled = true;
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

        StartCoroutine(AttackCooldown(_cooldownTime));

        this.GetComponent<Seek>().enabled = true;
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

        StartCoroutine(AttackCooldown(_cooldownTime));

        this.GetComponent<Seek>().enabled = true;
    }

    // ------ COROUTINES: ------

    public override IEnumerator GetHitEffect()
    {
        _hit.Play(); // Hit SFX

        _spriteRenderer.color = new Color(0, 255, 0);
        _spriteRenderer2.color = new Color(0, 255, 0);
        _spriteRenderer3.color = new Color(0, 255, 0);

        yield return new WaitForSeconds(_hitEffectDuration); // Wait

        _spriteRenderer.color = _sprite1Color;
        _spriteRenderer2.color = _sprite2Color;
        _spriteRenderer3.color = _sprite3Color;

        _gotHit = false;
    }

    IEnumerator Charging(float seconds)
    {
        Debug.Log("BloodBat->Charging");

        this.GetComponent<Seek>().enabled = false;

        _chargeDirection = _player.GetComponent<Transform>().position - this.gameObject.transform.position;
        _chargeDirection.Normalize();

        _attackIndicator.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        // Random attack move: (between 3 attacks)
        int _randomNumber = Random.Range(0, 7); // min included, max excluded

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
    }

    IEnumerator AttackCooldown(float seconds)
    {
        Debug.Log("BloodBat->Cooldown started");

        _spriteRenderer.color = new Color(0, 0, 255);

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("BloodBat->Cooldown finished");

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;
    }
}

