using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBoundary : MonoBehaviour
{
    [SerializeField] private float zRange = 20;
    [SerializeField] private float xRange = 30;
    [SerializeField] private float yRange = 10;
    public void DestroyOutOfBounds()
    {
        if (transform.position.x < -xRange || transform.position.x > xRange)
            Destroy(gameObject);
        if (transform.position.z < -zRange || transform.position.z > zRange)
            Destroy(gameObject);
        if (transform.position.y < -yRange)
            Destroy(gameObject);
    }
}
