using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRigidbody;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float enemyShootDelay;
    public GameObject arrowPrefab;
    private Animator enemyAnimator;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyShootDelay = Random.Range(1.5f, 3f);
        enemyAnimator = GetComponent<Animator>();
        InvokeRepeating("Shoot", enemyShootDelay, enemyShootDelay);
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
            enemyAnimator.SetTrigger("shoot_trig");
            Instantiate(arrowPrefab, new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
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



        // If not using physics:

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateEnemy, rotateSpeed);
        // If not caring about rotation speed:
        //transform.LookAt(lookDirection);
    }

}
