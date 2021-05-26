using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Rigidbody arrowRigidbody;
    public float speed = 20f;
    public float torque = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }
    
    
    // Add forward force and torque to arrow modified with speed
    public void MoveArrow()
    {
        arrowRigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
        arrowRigidbody.AddRelativeTorque(0, 0, torque);
    }
}