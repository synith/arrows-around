using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 1;
    private bool arrowHit;
    private ArrowController arrowController;

    private void Awake()
    {
        arrowController = gameObject.GetComponent<ArrowController>();
    }    
    void Start()
    {
        arrowHit = false;
    }    
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {            
            if (contact.otherCollider.CompareTag("Shield") && !arrowHit)
            {
                ++arrowController.playerController.shieldHits;
                Debug.Log("Shield Hit");
                ArrowHit();
                StartCoroutine(DespawnArrow());
            }
            else if (contact.otherCollider.CompareTag("Body") && !arrowHit)
            {
                ++arrowController.playerController.bodyHits;
                Debug.Log("Body Hit");
                ArrowHit();
                StartCoroutine(DespawnArrow());
            }
            else if (!arrowHit)
            {
                ArrowHit();
                StartCoroutine(DespawnArrow());
            }
        }
    }
    private void ArrowHit()
    {
        arrowHit = true;
        arrowController.arrowMovement.speed /= 2;
        arrowController.arrowMovement.torque = 0;
        arrowController.arrowMovement.arrowRigidbody.constraints = RigidbodyConstraints.None;
        arrowController.arrowMovement.arrowRigidbody.useGravity = true;
    }
    IEnumerator DespawnArrow()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }
}
