using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MovementDirection { NORTH, SOUTH, WEST, EAST, NW, NE, SW, SE, NOPARTICLES };

public class Player_Movement_Script : MonoBehaviour
{
// ------ PUBLIC: ------

    public int _healthValue;
    public float _movementSpeed;
    public float _dashDistance;

    public Rigidbody2D _rigidBody;
    public Animator _animator;
    public ParticleSystem _runningParticles;
    public ParticleSystem _dashParticles;

// ------ PRIVATE: ------

    private bool _isPlayerWalking = false;
    private bool _isPlayerDashing = false;
    private bool _canPlayerDash = true;
    // private bool _isPlayerInvincible = false;

    private MovementDirection movementDirection;

    private Vector2 movement; // X, and Y;

// ------ START / UPDATE / FIXEDUPDATE: ------

    // Update is called once per frame;
    void Update()
    {
        if (_canPlayerDash) { StartCoroutine(Dash()); }

        ProcessInputs();

        ProcessAnimations();

        ProcessParticles();

        ProcessAudio();
    }

    private void FixedUpdate()
    {
        Move();
    }

// ------ METHODS: ------

    void ProcessInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Value between -1 and 1 (by default this works with the arrow keys and WASD);
        movement.y = Input.GetAxisRaw("Vertical"); // Value between -1 and 1 (by default this works with the arrow keys and WASD);

        movement = movement.normalized; // We normalize the vector2 to avoid moving faster Diagonally than Horiontally or Vertically;
        // If we don't normalize it, when we move diagonally we will get a magnitude of the square root of 2, which is 1.4, which is
        // 40% greater than the maximum horizontal or vertical speeds;

        if (Input.GetKeyDown(KeyCode.U)) // Hit effect -> Just for testing purposes
            _rigidBody.AddForce(Vector2.up * 1000f);

        if (Input.GetKey(KeyCode.LeftShift)) // Walking -> Just for testing purposes
            _isPlayerWalking = true;

        if (Input.GetKeyDown(KeyCode.Space) && _canPlayerDash) // Dash -> Just for testing purposes
            _isPlayerDashing = true;
    }

    void ProcessAnimations()
    {
        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void ProcessParticles()
    {
        // If the player is walking, don't emit particles.

        _runningParticles.transform.right = -_rigidBody.velocity;
    }

    void ProcessAudio()
    {
        
    }

    void Move()
    {
        if (!_isPlayerWalking)
            _rigidBody.AddForce(movement * _movementSpeed);
        else
        {
            _rigidBody.AddForce((movement * _movementSpeed) / 2);
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

    public void Heal(int value)
    {
        _healthValue += value;
    }

// ------ COROUTINES: ------

    IEnumerator Dash()
    {
        if (_isPlayerDashing)
        {
            if (movement.magnitude != 0) // Moving.
            {
                Debug.Log("Dashed");

                _canPlayerDash = false;
                // _isPlayerInvincible = true;

                _dashParticles.Play();

                _rigidBody.AddForce(movement * _movementSpeed * _dashDistance);

                _isPlayerDashing = false;

                yield return new WaitForSeconds(5);

                Debug.Log("You can Dash again");
                _canPlayerDash = true;
            }
            else { _isPlayerDashing = false; }
        }
    }
}
