using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public AudioSource playerAudio;
    public bool
        isDead = false,
        isHit = false,
        shieldIsHit = false,
        shieldUp = false;
    public int
        shieldHits = 0,
        bodyHits = 0;
    
    private Rigidbody playerRb;
    private Vector3 moveDirection;
    private GameObject
        shieldObject,
        bodyObject;
    private Animator playerAnimator;
    private GameManager gameManager;
    private bool
        deathCry = false,
        shieldBroken;
    private readonly int
        playerMaxhp = 5,
        shieldMaxhp = 3;
    private int
        playerHp = 5,
        shieldHp = 3;

    [SerializeField]
    private float
        speed,
        rotateSpeed,
        xRange,
        zRange;
    [SerializeField]
    private HealthBar
        healthBar,
        shieldbar;
    [SerializeField]
    private AudioClip
        deathSound,
        hitSound,
        pickupSound;
    [SerializeField]
    private ParticleSystem
        bloodParticle,
        deathParticle;
    
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        shieldObject = GameObject.Find("Shield");
        bodyObject = GameObject.Find("Body");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    public void PlayerControllerStart()
    {
        playerRb = GetComponent<Rigidbody>();        
        shieldObject.SetActive(false);
        healthBar.SetMaxHealth(playerMaxhp);
        shieldbar.SetMaxHealth(shieldMaxhp);
        bodyHits = 0;
        shieldHits = 0;        
    }
    private void Update()
    {
        PlayerInput();
        ConstrainPlayer();
        ShieldBlock();
    }
    private void FixedUpdate()
    {
        CheckPlayerHP();
        CheckShieldHP();
        if (!gameManager.gameOver && gameManager.gameStarted)
        {
            CheckShieldBroken();
            MovePlayer();
            RotateTowardsMovementDirection();
        }
        CheckIfHit();
    }    
    private void PlayerInput()
    {        
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
    private void ShieldBlock()
    {
        // Shield Block on space bar or left mouse button
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
            shieldUp = true;
        else
            shieldUp = false;
    }
    private void CheckPlayerHP()
    {
        playerHp = playerMaxhp - bodyHits;

        if (playerHp < 1)
        {
            gameManager.gameOver = true;
            TimerController.instance.EndTimer();
            isDead = true;
            gameManager.gameOverUI.gameObject.SetActive(true);
            playerHp = 0;
        }
    }
    private void CheckShieldHP()
    {
        shieldHp = shieldMaxhp - shieldHits;

        if (shieldHp <= 0)
            shieldBroken = true;

        if (shieldHp > 0)
            shieldBroken = false;
    }
    private void CheckShieldBroken()
    {
        // if shield is broken space bar does nothing
        if (!shieldBroken)
            shieldObject.SetActive(shieldUp);
        else
            shieldObject.SetActive(false);
    }
    private void MovePlayer()
    {
        playerRb.AddForce(moveDirection * speed);
    }
    // Rotates player to direction they are moving in
    private void RotateTowardsMovementDirection()
    {
        if (moveDirection.magnitude != 0)
        {
            playerAnimator.SetTrigger("walking_trig");
            Quaternion rotatePlayer = Quaternion.LookRotation(moveDirection);
            playerRb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotatePlayer, rotateSpeed));
        }
    }
    private void CheckIfHit()
    {
        if (isHit)
        {
            healthBar.SetHealth(playerHp);

            if (isDead)
            {
                if (!deathCry)
                {
                    playerAudio.PlayOneShot(deathSound, 0.1f * gameManager.volume);
                    deathParticle.Play();
                    bodyObject.SetActive(false);
                    shieldObject.SetActive(false);
                    deathCry = true;
                }
            }
            else
            {
                playerAudio.PlayOneShot(hitSound, 0.2f * gameManager.volume);
                bloodParticle.Play();
                isHit = false;
            }
        }
        if (shieldIsHit)
        {
            shieldbar.SetHealth(shieldHp);
            shieldIsHit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            --shieldHits;

            if (shieldHits < 0)
                shieldHits = 0;
            else if (shieldHits > shieldMaxhp)
                shieldHits = shieldMaxhp;

            if (shieldHp >= 0 && shieldHp < shieldMaxhp)
            {
                Destroy(other.gameObject);
                shieldIsHit = true;
                playerAudio.PlayOneShot(pickupSound, 0.3f * gameManager.volume);
            }
        }
    }
}
