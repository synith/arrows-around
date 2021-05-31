using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBoundary : MonoBehaviour
{
    [SerializeField] private float zRange = 20;
    [SerializeField] private float xRange = 30;
    [SerializeField] private float yRange = 10;
    private ArrowController arrowController;

    private void Awake()
    {
        arrowController = GetComponent<ArrowController>();
    }
    public void DestroyOutOfBounds()
    {
        if (transform.position.x < -xRange || transform.position.x > xRange)
            arrowController.ReturnToPool();
        if (transform.position.z < -zRange || transform.position.z > zRange)
            arrowController.ReturnToPool();
        if (transform.position.y < -yRange)
            arrowController.ReturnToPool();
    }
}
