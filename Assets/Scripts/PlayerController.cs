using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float
        speed,
        xRange,
        zRange,
        rotateSpeed;

    private Vector3 moveDirection;
    private Rigidbody playerRb;

    public int
        shieldHits,
        bodyHits;

    public bool shieldUp;
    private GameObject shieldObject;

    private Animator playerAnimator;
    private GameManager gameManager;

    // Start is called before the first frame update
    public void PlayerControllerStart()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        shieldObject = GameObject.Find("Shield");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        shieldUp = false;
        shieldHits = 0;
        bodyHits = 0;
        shieldObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        PlayerInput();

        // Boundary
        ConstrainPlayer();


        // Shield Block
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
            shieldUp = true;
        else
            shieldUp = false;
    }
    private void FixedUpdate()
    {
        // if game not over
        if (!gameManager.gameOver)
        {
            // if shield not broken, allow shield to spawn.
            if (!gameManager.shieldBroken)
                shieldObject.SetActive(shieldUp);
            else
                shieldObject.SetActive(false);

            // Movement
            playerRb.AddForce(moveDirection * speed);

            // Rotation
            RotateTowardsMovementDirection();
        }

    }

    // Rotates player to direction they are moving in
    private void RotateTowardsMovementDirection()
    {
        // if moving in a direction
        if (moveDirection.magnitude != 0)
        {
            // Quaternion using vector to represent where we want to rotate to
            Quaternion rotatePlayer = Quaternion.LookRotation(moveDirection);

            // With physics:
            // rotate rigidbody from current rotation to target rotation at a set speed degrees/second
            playerRb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotatePlayer, rotateSpeed));

            // Without physics:
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotatePlayer, rotateSpeed);


            playerAnimator.SetBool("moving_b", true);
        }
        else
            playerAnimator.SetBool("moving_b", false);
    }

    // Sets horizontal and vertical input as a vector
    private void PlayerInput()
    {
        // a vector based on our x and y input
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
    }


    // If the player goes outside of boundary set player position to the boundary
    private void ConstrainPlayer()
    {
        if (transform.position.x < -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        if (transform.position.z < -zRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        if (transform.position.z > zRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            Debug.Log("So Stronk");
            --shieldHits;

            if (shieldHits < 0)
                shieldHits = 0;
            else if (shieldHits > 5)
                shieldHits = 5;

            if (gameManager.shieldHp >= 0 && gameManager.shieldHp < 5)
                Destroy(other.gameObject);
        }
    }

}
