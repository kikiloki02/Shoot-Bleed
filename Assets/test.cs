using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Vector2 movement;
    public Rigidbody2D _rigidBody; 
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Value between -1 and 1 (by default this works with the arrow keys and WASD);
        movement.y = Input.GetAxisRaw("Vertical"); // Value between -1 and 1 (by default this works with the arrow keys and WASD);

        movement = movement.normalized; // We normalize the vector2 to avoid moving faster Diagonally than Horiontally or Vertically;
        // If we don't normalize it, when we move diagonally we will get a magnitude of the square root of 2, which is 1.4, which is
        // 40% greater than the maximum horizontal or vertical speeds;

        if (Input.GetKeyDown(KeyCode.U)) // Hit effect -> Just for testing purposes
            _rigidBody.AddForce(Vector2.up * 1000f);

        if (_rigidBody.velocity.magnitude > 1f)
        {
                float maxSpeed = Mathf.Lerp(_rigidBody.velocity.magnitude, 1f, Time.fixedDeltaTime * 5f);
                _rigidBody.velocity = (_rigidBody.velocity.normalized * maxSpeed) / 2;
            
        }
    }
}
