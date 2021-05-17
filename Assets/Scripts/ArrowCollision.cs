using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 1;
    private bool arrowHit;
    private ArrowController arrowController;

    private AudioSource arrowAudio;
    [SerializeField]private AudioClip arrowShield;
    [SerializeField]private AudioClip arrowFlesh;

    [SerializeField] private ParticleSystem sparkParticle;    

    private void Awake()
    {
        arrowController = GetComponent<ArrowController>();
        arrowAudio = GetComponent<AudioSource>();
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
                if (!arrowController.gameManager.gameOver)
                    ++arrowController.playerController.shieldHits;

                ArrowHit();
                arrowAudio.PlayOneShot(arrowShield, 0.1f);
                arrowController.playerController.shieldIsHit = true;
                sparkParticle.Play();
                StartCoroutine(DespawnArrow());
            }
            else if (contact.otherCollider.CompareTag("Body") && !arrowHit)
            {
                if (!arrowController.gameManager.gameOver)
                    ++arrowController.playerController.bodyHits;

                ArrowHit();
                arrowAudio.PlayOneShot(arrowFlesh, 0.1f);
                arrowController.playerController.isHit = true;
                StartCoroutine(DespawnArrow());
            }
            else if (!arrowHit)
            {
                ArrowHit();
                StartCoroutine(DespawnArrow());

                // play arrow hitting eachother noise here
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
