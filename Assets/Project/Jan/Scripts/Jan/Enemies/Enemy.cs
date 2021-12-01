using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
// ------ PUBLIC: ------

    public int _attackValue;
    public int _movementSpeed;
    public int _chargeDistance;
    public int _healPlayer;

    // Cooldown related variables:
    public float _attack1ChargeTime;
    public float _attack2ChargeTime;
    public float _attack3ChargeTime;
    public float _activeTimeAttack1;
    public float _activeTimeAttack2;
    public float _activeTimeAttack2Forth;
    public float _activeTimeAttack2Back;
    public float _activeTimeAttack3;
    public float _activeTimeAttack3First;
    public float _activeTimeAttack3Second;
    public float _attack1Cooldown;
    public float _attack2Cooldown;
    public float _attack2point5Cooldown;
    public float _attack3Cooldown;
    public float _attack3point5Cooldown;

    public ParticleSystem _chargingParticlesBasic;
    public ParticleSystem _chargingParticlesForthAndBack;
    public ParticleSystem _chargingParticlesChain;
    public ParticleSystem _attackParticles;

    public Rigidbody2D _rigidBody;
    public Collider2D _attackDetectionZone;
    public SpriteRenderer _spriteRenderer;
    public GameObject _player;

    public AudioSource _attack1;
    public AudioSource _attack2;
    public AudioSource _charge;
    public AudioSource _hit;
    public AudioSource _death;

    public GameObject _attackPivot;
    // TODO Change this for a Collider2D and find how to execute OnTriggerStay2D() with this specific collider.
    public GameObject _attackIndicator;

// ------ PROTECTED: ------

    protected float _hitEffectDuration = 0.075f;
    protected bool _gotHit;
    protected Vector2 _chargeDirection;
    protected Color32 _spriteRedColor;
    protected Color32 _spriteBlueColor;
    protected Color32 _spriteWhiteColor;
    protected bool _canAttack = true;

    protected bool _isCharging = false;
    protected bool _cooldown = false;

    protected int _randomNumber;
    protected bool doRandom = true;

    protected bool _rotate;

// ------ METHODS: ------

    public virtual void Die() {}

    public virtual void HealPlayer() {}

    public virtual void GetHit() {}

    public virtual IEnumerator GetHitEffect() { yield return new WaitForSeconds(0f); }
}