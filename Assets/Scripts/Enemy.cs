using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float enemyShootDelay;
    private GameObject player;
    private Rigidbody enemyRigidbody;
    public GameObject arrowPrefab;
    [SerializeField] private AudioClip enemySpawnSound;
    [SerializeField] private AudioClip enemyDrawSound;
    [SerializeField] private AudioClip enemyShootSound;
    private AudioSource enemyAudio;
    private Animator enemyAnimator;
    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyShootDelay = Random.Range(1.5f, 3f);
        InvokeRepeating("Shoot", enemyShootDelay, enemyShootDelay);
        InvokeRepeating("ShootSound", enemyShootDelay - 0.3f, enemyShootDelay);
        enemyAudio.PlayOneShot(enemySpawnSound, 0.1f * gameManager.volume);
    }

    // Physics Update()
    private void FixedUpdate()
    {
        RotateTowardsPlayer();
    }
    void Shoot()
    {
        if (!gameManager.gameOver)
        {
            enemyAudio.PlayOneShot(enemyShootSound, 0.1f * gameManager.volume);
            Instantiate(arrowPrefab, new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
        }
    }
    void ShootSound()
    {
        if (!gameManager.gameOver)
        {
            enemyAnimator.SetTrigger("shoot_trig");
            enemyAudio.PlayOneShot(enemyDrawSound, 0.1f * gameManager.volume);
        }

    }
    void RotateTowardsPlayer()
    {
        // Sets a vector of magnitude 1 towards the player from the enemy.
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Quaternion using vector to represent where we want to rotate to
        Quaternion rotateEnemy = Quaternion.LookRotation(lookDirection);

        // rotate rigidbody from current rotation to target rotation at a set speed degrees/second
        enemyRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotateEnemy, rotateSpeed));



        /* If not using physics:
         * transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateEnemy, rotateSpeed);
         * 
         * If not caring about rotation speed:
         * transform.LookAt(lookDirection);
         */
    }

}
