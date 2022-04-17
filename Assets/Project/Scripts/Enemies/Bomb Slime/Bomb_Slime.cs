using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Slime : Enemy
{
    public float _buryAnimationDuration;
    public float _reactionTime; // Time that the Player will have to dodge the Bomb Slime's Attack.

    public GameObject _bomb;

    [SerializeField] bool _followPlayer;

// ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start() // DONE!
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
        if (_followPlayer)
        {
            this.transform.position = _player.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // DONE!
    {
        if (collision.gameObject.tag == "Player" && _canAttack)
        {
            StartCoroutine(Charging(_attack1ChargeTime));
        }
    }

    private void OnCollisionStay2D(Collision2D collision) // DONE!
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().GetDamage(_collisionDamageValue);
        }
    }

// ------ METHODS: ------

    public override void Die() // DONE!
    {
        AudioSource.PlayClipAtPoint(_death.clip, Camera.main.transform.position, 0.2f);

        // Death particles:
    }

    public override void GetHit() // DONE!
    {
        _gotHit = true;

        StartCoroutine(GetHitEffect());
    }

    void Attack() // DONE! (I think)
    {
        // The Bomb Slime shoots out of the ground.

        GetComponent<SpriteRenderer>().enabled = true; // Make the Bomb Slime visible again.

        StartCoroutine(ActivateCapsuleCollider2D(0.15f));

        GetComponent<Animator>().SetTrigger("Attack"); // Play the Unbury animation.

        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(false); // Hide the indicator.

        _attackParticles.Play(); // Attack particles.

        _attack1.Play(); // Attack1 SFX.

        // The Attack move:
        _attackPivot.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, _chargeDirection));

        // Activate the Collider that checks if the Player is inside the attack's range.
            // If the Player is inside -> Deal Damage (this is already done by the GameObject itself).
        StartCoroutine(AttackColliderSwitch(0, _activeTimeAttack1)); 

        StartCoroutine(AttackCooldown(_attack1Cooldown)); // Start the cooldown (until it finishes, the enemy won't attack again).

        this.GetComponent<Seek>().enabled = true; // Follow the Player again.
    }

    void BuryUnderground() // DONE!
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;

        // Follow the Player while being underground:
        _followPlayer = true;

        // Drop a bomb:
        Instantiate(_bomb, new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z), Quaternion.Euler(0, 0, 0));
    }

    // ------ COROUTINES: ------

    IEnumerator ActivateCapsuleCollider2D(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    IEnumerator PreparedToAttack(float time)
    {
        // Stop following the Player (while being underground):
        _followPlayer = false;
        _attackIndicator.GetComponent<AttackPivot_Manager>()._attacks[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        Attack();
    }

    public override IEnumerator GetHitEffect() // DONE!
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
        GetComponent<Animator>().SetTrigger("Bury");

        _isCharging = true;

        Invoke("BuryUnderground", _buryAnimationDuration); // Deactivates SpriteRenderer after _buryAnimationDuration seconds.

        this.GetComponent<Seek>().enabled = false; // Don't follow the Player

        _charge.Play(); // Charge1 SFX

        // Logic:

        _canAttack = false;

        yield return new WaitForSeconds(seconds); // Wait

            _spriteRenderer.color = new Color(255, 255, 255);

            // Execute the corresponding attack move:

            StartCoroutine(PreparedToAttack(_reactionTime));

        _isCharging = false;

    }

    IEnumerator AttackCooldown(float seconds) // DONE!
    {
        _cooldown = true;

        _spriteRenderer.color = _spriteBlueColor;

        yield return new WaitForSeconds(seconds); // Wait

        _spriteRenderer.color = new Color(255, 255, 255);

        _canAttack = true;

        _cooldown = false;
    }

    IEnumerator AttackColliderSwitch(int attack, float secondsActive) // DONE!
    {
        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[attack].gameObject.SetActive(true);

        yield return new WaitForSeconds(secondsActive);

        _attackPivot.GetComponent<AttackPivot_Manager>()._attacks[attack].gameObject.SetActive(false);
    }
}

