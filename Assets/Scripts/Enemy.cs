using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRigidbody;
    public float rotateSpeed;
    public float enemyShootDelay;
    public GameObject arrowPrefab;
    private bool canShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(ShotDelay());
        enemyShootDelay = Random.Range(1.5f, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (canShoot)
        {
            Instantiate(arrowPrefab, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation);
            canShoot = false;
            StartCoroutine(ShotDelay());
        }
    }
    // Physics Update()
    private void FixedUpdate()
    {
        RotateTowardsPlayer();       

    }

    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(enemyShootDelay);
        canShoot = true;

    }

    // Rotates rigidbody towards player at a set speed
    void RotateTowardsPlayer()
    {
        // Sets a vector towards the player from the enemy. Normalized:limit magnitude to 1
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        // Quaternion using vector to represent where we want to rotate to
        Quaternion rotateEnemy = Quaternion.LookRotation(lookDirection);

        // If using physics:
        // rotate rigidbody from current rotation to target rotation at a set speed degrees/second
        enemyRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotateEnemy, rotateSpeed));

        // If not using physics:
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateEnemy, rotateSpeed);
        // If not caring about rotation speed:
        //transform.LookAt(lookDirection);

    }
}
