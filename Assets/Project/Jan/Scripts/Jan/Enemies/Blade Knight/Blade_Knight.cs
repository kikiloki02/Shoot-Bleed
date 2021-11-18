using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_Knight : Enemy
{
    public ParticleSystem _chargingParticlesBasic;
    public ParticleSystem _chargingParticlesForthAndBack;
    public ParticleSystem _chargingParticlesChain;
    public ParticleSystem _attackParticles;

    //public AudioManager _audioManager;

    public GameObject _attackPivot;
    // TODO Change this for a Collider2D and find how to execute OnTriggerStay2D() with this specific collider.
    public GameObject _attackIndicator;

    public AudioSource _attack1;
    public AudioSource _attack2;
    public AudioSource _charge;
    public AudioSource _hit;
    public AudioSource _death;

    private bool doRandom;
    private int _randomNumber;

    // TODO Modify the Methods so that they are more compatible with AttackColliderSwitch coroutine.

    // ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;

        _spriteRedColor = new Color32(255, 50, 50, 255);
        _spriteBlueColor = new Color32(0, 0, 255, 255);
        _spriteWhiteColor = new Color32(255, 255, 255, 255);
    }

    private void Update()
    {
        // Tests:
        if (Input.GetKeyDown(KeyCode.Y)) { GetHit(); }
        if (Input.GetKeyDown(KeyCode.R)) { _attack1.Play(); ; } // Should be: AudioManager.instance.PlaySFX(value);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            doRandom = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            doRandom = false;
            _randomNumber = 0;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            doRandom = false;
            _randomNumber = 3;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            doRandom = false;
            _randomNumber = 5;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack) { StartCoroutine(Charging(_chargeTime)); }
    }

// ------ METHODS: ------

    public override void Die()
    {
        Debug.Log("Blade Knight->Dead");

        _death.Play(); // Die SFX
    }

    public override void GetHit()
    {
        _gotHit = true;

        StartCoroutine(GetHitEffect());
    }

    void Attack1()
    {
        Debug.Log("Blade Knight->Attack1");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        StartCoroutine(AttackColliderSwitch(0, 0.5f));
        // ------

        StartCoroutine(AttackCooldown(_cooldownTime));

        this.GetComponent<Seek>().enabled = true;
    }

    void Attack2(float seconds)
    {
        Debug.Log("Blade Knight->Attack2");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[1].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack1.Play(); // Attack1 SFX

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        StartCoroutine(AttackColliderSwitch(1, 0.5f));
        // ------

        StartCoroutine(AttackCooldown(_cooldownTime));

        this.GetComponent<Seek>().enabled = true;
    }

    void Attack3(float seconds)
    {
        Debug.Log("Blade Knight->Attack3");

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[2].gameObject.SetActive(false);

        _attackParticles.Play();

        _attack2.Play(); // Attack2 SFX

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        StartCoroutine(AttackColliderSwitch(2, 0.5f));
        // ------

        StartCoroutine(AttackCooldown(_cooldownTime));

        this.GetComponent<Seek>().enabled = true;
    }

// ------ COROUTINES: ------

    public override IEnumerator GetHitEffect()
    {
        _hit.Play(); // Hit SFX

        _spriteRenderer.color = _spriteRedColor;
        _spriteRenderer2.color = _spriteRedColor;
        _spriteRenderer3.color = _spriteRedColor;

        yield return new WaitForSeconds(_hitEffectDuration); // Wait

        if (_isCharging)
        {
            _spriteRenderer.color = new Color32(255, 0, 0, 255);
            _spriteRenderer2.color = new Color32(0, 0, 0, 255);
            _spriteRenderer3.color = _spriteWhiteColor;
        }
        else if (_cooldown)
        {
            _spriteRenderer.color = _spriteBlueColor;
            _spriteRenderer2.color = new Color32(0, 0, 0, 255);
            _spriteRenderer3.color = _spriteWhiteColor;
        }
        else
        {
            _spriteRenderer.color = _spriteWhiteColor;
            _spriteRenderer2.color = new Color32(0, 0, 0, 255);
            _spriteRenderer3.color = _spriteWhiteColor;
        }

        _gotHit = false;
    }

    IEnumerator Charging(float seconds)
    {
        Debug.Log("Blade Knight->Charging");

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

        Debug.Log("Blade Knight->Finished charging");

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
        Debug.Log("Blade Knight->Cooldown started");

        _cooldown = true;

        _spriteRenderer.color = new Color(0, 0, 255);

        yield return new WaitForSeconds(seconds); // Wait

        Debug.Log("Blade Knight->Cooldown finished");

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

