using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float
        speed = 10000,
        xRange = 13,
        zRange = 13,
        rotateSpeed = 15;
    private Vector3 moveDirection;
    private Rigidbody playerRb;    
    public bool shieldUp;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        PlayerInput();

        if (Input.GetKey(KeyCode.Space))
        {
            shieldUp = true;
        }
        else
        {
            shieldUp = false;
        }
    }
    private void FixedUpdate()
    {
        // Movement
        playerRb.AddForce(moveDirection * speed);
        ConstrainPlayer();

        // Rotation
        RotateTowardsMovementDirection();        
    }

    // Rotates player to direction they are moving in
    private void RotateTowardsMovementDirection()
    {
        if (moveDirection.magnitude != 0)
        {
            Quaternion rotatePlayer = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotatePlayer, rotateSpeed);
        }
    }

    // Sets horizontal and vertical input as a vector
    private void PlayerInput()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
    }


    // If the player goes outside of boundary set player position to the boundary
    private void ConstrainPlayer()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }
}
