using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MovementDirection { NORTH, SOUTH, WEST, EAST, NW, NE, SW, SE, NOPARTICLES };

public class Player_Movement_Script : MonoBehaviour
{
    public float _movementSpeed;
    public float _dashDistance;

    public Rigidbody2D _rigidBody;
    public Animator _animator;

    public ParticleSystem _runningParticlesNorth;
    public ParticleSystem _runningParticlesSouth;
    public ParticleSystem _runningParticlesWest;
    public ParticleSystem _runningParticlesEast;
    public ParticleSystem _runningParticlesNorthWest;
    public ParticleSystem _runningParticlesNorthEast;
    public ParticleSystem _runningParticlesSouthWest;
    public ParticleSystem _runningParticlesSouthEast;
    public ParticleSystem _dashParticles;

    private bool _isPlayerWalking = false;
    private bool _isPlayerDashing = false;
    private bool _canPlayerDash = true;
    private bool _isPlayerInvincible = false;
    private bool _firstTime = true;

    MovementDirection movementDirection;

    Vector2 movement; // X, and Y;

    // ------ START / UPDATE / FIXEDUPDATE: ------

    // Start is called before the first frame update;
    void Start()
    {
        if (_firstTime)
        {
            _firstTime = false;

            _runningParticlesNorth.Stop();
            _runningParticlesSouth.Stop();
            _runningParticlesWest.Stop();
            _runningParticlesEast.Stop();
            _runningParticlesNorthWest.Stop();
            _runningParticlesNorthEast.Stop();
            _runningParticlesSouthWest.Stop();
            _runningParticlesSouthEast.Stop();
            _dashParticles.Stop();
        }
    }

    // Update is called once per frame;
    void Update()
    {
        if (_canPlayerDash) { StartCoroutine(Dash()); }

        ProcessInputs();

        ProcessAnimations();

        ProcessMovementDirection();

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
        switch (movementDirection)
        {
            case MovementDirection.NORTH:
                {
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("Up");
                    _runningParticlesNorth.Play();
                    break;
                }
            case MovementDirection.SOUTH:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("Down");
                    _runningParticlesSouth.Play();
                    break;
                }
            case MovementDirection.WEST:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("Left");
                    _runningParticlesWest.Play();
                    break;
                }
            case MovementDirection.EAST:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("Right");
                    _runningParticlesEast.Play();
                    break;
                }
            case MovementDirection.NW:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("NW");
                    _runningParticlesNorthWest.Play();
                    break;
                }
            case MovementDirection.NE:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("NE");
                    _runningParticlesNorthEast.Play();
                    break;
                }
            case MovementDirection.SW:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("SW");
                    _runningParticlesSouthWest.Play();
                    break;
                }
            case MovementDirection.SE:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();

                    Debug.Log("SE");
                    _runningParticlesSouthEast.Play();
                    break;
                }
            case MovementDirection.NOPARTICLES:
                {
                    _runningParticlesNorth.Stop();
                    _runningParticlesSouth.Stop();
                    _runningParticlesWest.Stop();
                    _runningParticlesEast.Stop();
                    _runningParticlesNorthWest.Stop();
                    _runningParticlesNorthEast.Stop();
                    _runningParticlesSouthWest.Stop();
                    _runningParticlesSouthEast.Stop();

                    Debug.Log("Nothing");
                    break;
                }
            default:
                {
                    break;
                }
        }
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

    void ProcessMovementDirection()
    {
        if (_isPlayerWalking) { movementDirection = MovementDirection.NOPARTICLES; return; }

        if (Input.GetAxisRaw("Vertical") > 0) // NORTH:
        {
            if (Input.GetAxisRaw("Horizontal") < 0) { movementDirection = MovementDirection.NW; } // NORTH WEST:
            else if (Input.GetAxisRaw("Horizontal") > 0) { movementDirection = MovementDirection.NE; } // NORTH EAST:
            else { movementDirection = MovementDirection.NORTH; } // JUST NORTH:

            return;
        }

        if (Input.GetAxisRaw("Vertical") < 0) // SOUTH:
        {
            if (Input.GetAxisRaw("Horizontal") < 0) { movementDirection = MovementDirection.SW; } // SOUTH WEST:
            else if (Input.GetAxisRaw("Horizontal") > 0) { movementDirection = MovementDirection.SE; } // SOUTH EAST:
            else { movementDirection = MovementDirection.SOUTH; } // JUST SOUTH:

            return;
        }

        if (Input.GetAxisRaw("Horizontal") < 0) // WEST:
        {
            if (Input.GetAxisRaw("Vertical") > 0) { movementDirection = MovementDirection.NW; } // NORTH WEST:
            else if (Input.GetAxisRaw("Vertical") < 0) { movementDirection = MovementDirection.SW; } // SOUTH WEST:
            else { movementDirection = MovementDirection.WEST; } // JUST WEST:

            return;
        }

        if (Input.GetAxisRaw("Horizontal") > 0) // EAST:
        {
            if (Input.GetAxisRaw("Vertical") > 0) { movementDirection = MovementDirection.NE; } // NORTH EAST:
            else if (Input.GetAxisRaw("Vertical") < 0) { movementDirection = MovementDirection.SE; } // SOUTH EAST:
            else { movementDirection = MovementDirection.EAST; } // JUST EAST:

            return;
        }

        movementDirection = MovementDirection.NOPARTICLES;
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
                _isPlayerInvincible = true;

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
