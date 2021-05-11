using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private ArrowBoundary arrowBoundary;
    public ArrowMovement arrowMovement;
    public PlayerController playerController;
    public GameManager gameManager;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        arrowBoundary = gameObject.GetComponent<ArrowBoundary>();
        arrowMovement = gameObject.GetComponent<ArrowMovement>();
    }    
    private void FixedUpdate()
    {
        arrowBoundary.DestroyOutOfBounds();
        if (!gameManager.gameOver)
        {
            arrowMovement.MoveArrow();
        }
        
    }
}