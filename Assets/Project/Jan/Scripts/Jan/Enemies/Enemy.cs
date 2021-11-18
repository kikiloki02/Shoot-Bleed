using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
// ------ PUBLIC: ------

    public int _attackValue;
    public int _movementSpeed;
    public int _chargeDistance;
    public int _healPlayer;
    public float _chargeTime;
    public float _cooldownTime;

    public Rigidbody2D _rigidBody;
    public Collider2D _attackDetectionZone;
    public SpriteRenderer _spriteRenderer;
    public SpriteRenderer _spriteRenderer2;
    public SpriteRenderer _spriteRenderer3;
    public GameObject _player;

// ------ PROTECTED: ------

    protected float _hitEffectDuration = 0.15f;
    protected bool _gotHit;
    protected Vector2 _chargeDirection;
    protected Color32 _spriteRedColor;
    protected Color32 _spriteBlueColor;
    protected Color32 _spriteWhiteColor;
    protected bool _canAttack = true;

    protected bool _isCharging = false;
    protected bool _cooldown = false;

    // ------ METHODS: ------

    public virtual void Die() {}

    public virtual void HealPlayer() {}

    public virtual void GetHit() {}

    public virtual IEnumerator GetHitEffect() { yield return new WaitForSeconds(0f); }
}