using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody arrowRigidbody;
    [SerializeField] float speed = 20f;
    [SerializeField] float zRange = 20;
    [SerializeField] float xRange = 30;
    [SerializeField] float despawnTimer = 1;
    public bool arrowHit;
    private PlayerController playerControllerscript;

    // Start is called before the first frame update
    void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        playerControllerscript = GameObject.Find("Player").GetComponent<PlayerController>();
        arrowHit = false;
    }

    private void FixedUpdate()
    {
        arrowRigidbody.AddRelativeForce(Vector3.forward * speed);
        arrowRigidbody.AddRelativeTorque(0, 0, 2);
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
        foreach (ContactPoint c in collision.contacts)
        {
            Debug.Log(c.otherCollider.name);
            if (c.otherCollider.CompareTag("Shield") && !arrowHit)
            {
                ++playerControllerscript.shieldHits;
                Debug.Log("Shield Hit");
                ArrowHit();
                StartCoroutine(DespawnArrow());
            }
            else if (c.otherCollider.CompareTag("Body") && !arrowHit)
            {
                ++playerControllerscript.bodyHits;
                Debug.Log("Body Hit");
                ArrowHit();
                StartCoroutine(DespawnArrow());
            }
            else
            {
                ArrowHit();
                StartCoroutine(DespawnArrow());
            }
        }

    }



    private void ArrowHit()
    {
        arrowHit = true;
        speed /= 2;
        arrowRigidbody.constraints = RigidbodyConstraints.None;
        arrowRigidbody.useGravity = true;

    }

}