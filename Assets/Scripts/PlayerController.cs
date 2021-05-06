using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float
        speed = 10000,
        xRange = 13,
        zRange = 13,
        rotateSpeed;
    private Vector3 moveDirection;
    private Rigidbody playerRb;    
    public bool shieldUp;
    private GameObject shieldObject;
    public int
        shieldHits,
        bodyHits;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI shieldText;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        shieldObject = GameObject.Find("Shield");
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

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            shieldUp = true;            
        }
        else
        {
            shieldUp = false;
        }

        bodyText.text = "Body: " + bodyHits;
        shieldText.text = "Shield: " + shieldHits; 
        
    }
    private void FixedUpdate()
    {
        shieldObject.SetActive(shieldUp);

        // Movement
        playerRb.AddForce(moveDirection * speed);
        ConstrainPlayer();

        // Rotation
        RotateTowardsMovementDirection();        
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
            Destroy(other.gameObject);
        }
    }
    
}
