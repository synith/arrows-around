using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private ArrowBoundary arrowBoundary;
    public ArrowMovement arrowMovement;
    public PlayerController playerController;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        arrowBoundary = gameObject.GetComponent<ArrowBoundary>();
        arrowMovement = gameObject.GetComponent<ArrowMovement>();
    }    
    private void FixedUpdate()
    {
        arrowBoundary.DestroyOutOfBounds();
        arrowMovement.MoveArrow();
    }
}