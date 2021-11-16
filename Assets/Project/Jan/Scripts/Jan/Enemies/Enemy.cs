using System.Collections;
using System.Collections.Generic;
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
    protected Color _sprite1Color;
    protected Color _sprite2Color;
    protected Color _sprite3Color;
    protected bool _canAttack = true;

    // ------ METHODS: ------

    public virtual void Die() {}

    public virtual void HealPlayer() {}

    public virtual void GetHit() {}

    public virtual IEnumerator GetHitEffect() { yield return new WaitForSeconds(0f); }
}