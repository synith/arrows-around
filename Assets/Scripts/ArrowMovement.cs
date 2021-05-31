using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Rigidbody arrowRigidbody;
    public float speed = 0.5f;
    public float torque = 2f;
    private Vector3 arrowStartPosition;
    private Quaternion arrowStartRotation;

    // Start is called before the first frame update
    void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        arrowStartPosition = GetComponent<Transform>().position;
        arrowStartRotation = GetComponent<Transform>().rotation;
    }    
    // Add forward force and torque to arrow modified with speed
    public void MoveArrow()
    {
        if (gameObject.activeSelf)
        {
            arrowRigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
            arrowRigidbody.AddRelativeTorque(0, 0, torque);
        }        
    }
    public void SetSpeed()
    {
        speed = 0.5f;
        torque = 2f;
        arrowRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        arrowRigidbody.velocity = Vector3.zero;
        arrowRigidbody.angularVelocity = Vector3.zero;
        arrowRigidbody.useGravity = false;
        arrowRigidbody.transform.SetPositionAndRotation(arrowStartPosition, arrowStartRotation);
    }
}