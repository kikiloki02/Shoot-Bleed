using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
// ------ PUBLIC: ------

    public int _healthValue;
    public float _movementSpeed;
    public float _dashDistance;
    public float _dashCooldown;
    public float _invincibilityTimeBetweenHitsInSeconds;
    public float _secondsPerBullet;
    public int _bloodBulletDamage;
    public int _normalBulletDamage;

    public Rigidbody2D _rigidBody;
    public Animator _animator;
    public ParticleSystem _runningParticles;
    public ParticleSystem _dashParticles;
    public AudioSource _dashSound;
    public RoomPos lastRoomExit;
    public GameObject _weapon;

// ------ PRIVATE: ------

    private bool _isPlayerWalking = false;
    private bool _isPlayerDashing = false;
    private bool _canPlayerDash = true;
    // private bool _isPlayerInvincible = false;

    private Vector2 _movement; // X, and Y;

    public class Upgrade
    {
        public int _id;
        public bool _active;

        public Upgrade(int id, bool active)
        {
            _id = id;
            _active = active;
        }
    };

    private List<Upgrade> _playerUpgrades = new List<Upgrade>();

// ------ START / UPDATE / FIXEDUPDATE: ------

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame;
    void Update()
    {
        if (_canPlayerDash) { StartCoroutine(Dash()); }

        ProcessInputs();

        ProcessAnimations();

        ProcessParticles();

        ProcessAudio();

        ProcessUpgrades();
    }

    private void FixedUpdate()
    {
        Move();
    }

// ------ METHODS: ------

    void ProcessInputs()
    {
        _movement.x = Input.GetAxisRaw("Horizontal"); // Value between -1 and 1 (by default this works with the arrow keys and WASD);
        _movement.y = Input.GetAxisRaw("Vertical"); // Value between -1 and 1 (by default this works with the arrow keys and WASD);

        _movement = _movement.normalized; // We normalize the vector2 to avoid moving faster Diagonally than Horiontally or Vertically;
        // If we don't normalize it, when we move diagonally we will get a magnitude of the square root of 2, which is 1.4, which is
        // 40% greater than the maximum horizontal or vertical speeds;

        if (Input.GetKeyDown(KeyCode.U)) // Hit effect -> Just for testing purposes
            _rigidBody.AddForce(Vector2.up * 1000f);

        if (Input.GetKey(KeyCode.LeftShift)) // Walking -> Just for testing purposes
            _isPlayerWalking = true;

        if (Input.GetKeyDown(KeyCode.Space) && _canPlayerDash) // Dash -> Just for testing purposes
            _isPlayerDashing = true;
    }

    public void ActivateUpgrade(int id)
    {
        Upgrade newUpgrade = new Upgrade(id, true);

        _playerUpgrades.Add(newUpgrade);
    }

    void ProcessAnimations()
    {
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    void ProcessParticles()
    {
        // If the player is walking, don't emit particles.

        _runningParticles.transform.right = -_rigidBody.velocity;
    }

    void ProcessAudio()
    {
        
    }

    void ProcessUpgrades()
    {
        for (int i = 0; i < _playerUpgrades.Count; i++)
        {
            if (!_playerUpgrades[i]._active) { continue; }

            switch (_playerUpgrades[i]._id)
            {
                case 1: // Wider Heart
                {
                    _playerUpgrades[i]._active = false;
                    this.GetComponent<PlayerLifeManagement>().maxHealth += 8;

                    this.GetComponent<PlayerLifeManagement>().healthBar.GetComponent<HealthBar>().SetMaxHealth(this.GetComponent<PlayerLifeManagement>().maxHealth);

                    break;
                }

                case 2: // Explosive Corpses
                {
                    // Code

                    break;
                }

                case 3: // Dash Guard
                {
                    // Code

                    break;
                }

                case 4: // Heavy Bullets
                {
                    _playerUpgrades[i]._active = false;

                    _secondsPerBullet += 0.2f;
                    _weapon.GetComponent<ShootBullet>().secondsPerBullet += 0.2f;

                    _bloodBulletDamage += 1;
                    _weapon.GetComponent<ShootBullet>().bullet.GetComponent<Bullet>().damage += 1;

                    break;
                }

                case 5: // Fast Shooting
                {
                    _playerUpgrades[i]._active = false;

                    _secondsPerBullet -= 0.15f;
                    _weapon.GetComponent<ShootBullet>().secondsPerBullet -= 0.15f;

                    break;
                }
            }
        }
    }

    void Move()
    {
        if (!_isPlayerWalking)
            _rigidBody.AddForce(_movement * _movementSpeed);
        else
        {
            _rigidBody.AddForce((_movement * _movementSpeed) / 2);
            _isPlayerWalking = false;
        }

        if (_rigidBody.velocity.magnitude > 1f)
        {
            if (!_isPlayerWalking)
            {
                float maxSpeed = Mathf.Lerp(_rigidBody.velocity.magnitude, 1f, Time.fixedDeltaTime * 5f);
                _rigidBody.velocity = _rigidBody.velocity.normalized * maxSpeed;
            }
            else
            {
                float maxSpeed = Mathf.Lerp(_rigidBody.velocity.magnitude, 1f, Time.fixedDeltaTime * 5f);
                _rigidBody.velocity = (_rigidBody.velocity.normalized * maxSpeed) / 2;
            }
        }
    }

// ------ COROUTINES: ------

    IEnumerator Dash()
    {
        if (_isPlayerDashing)
        {
            if (_movement.magnitude != 0) // Moving.
            {
                Debug.Log("Dashed");

                _canPlayerDash = false;

                // _isPlayerInvincible = true;

                _dashSound.Play();

                _dashParticles.Play();

                _rigidBody.AddForce(_movement * _movementSpeed * _dashDistance);

                _isPlayerDashing = false;

                yield return new WaitForSeconds(_dashCooldown);

                Debug.Log("You can Dash again");
                _canPlayerDash = true;
            }
            else { _isPlayerDashing = false; }
        }
    }
}
