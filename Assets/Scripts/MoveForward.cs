using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody arrowRigidbody;
    public float
        speed = 20f,
        zRange = 20,
        xRange = 30,
        despawnTimer = 3;
    public bool arrowHit;
    private PlayerController playerControllerscript;

    // Start is called before the first frame update
    void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        playerControllerscript = GameObject.Find("Player").GetComponent<PlayerController>();
        arrowHit = false;        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        arrowRigidbody.AddRelativeForce(Vector3.forward * speed);
        DestroyOutOfBounds();
    }

    IEnumerator DespawnArrow()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }

    private void DestroyOutOfBounds()
    {
        if (transform.position.x < -xRange)
            Destroy(gameObject);
        if (transform.position.x > xRange)
            Destroy(gameObject);
        if (transform.position.z < -zRange)
            Destroy(gameObject);
        if (transform.position.z > zRange)
            Destroy(gameObject);
        if (transform.position.y < -10)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            ++playerControllerscript.shieldHits;
            Destroy(gameObject);
        }
        else
        {
            speed /= 2;
            arrowRigidbody.constraints = RigidbodyConstraints.None;
            arrowRigidbody.useGravity = true;
            StartCoroutine(DespawnArrow());
        }
            
    }
      
}