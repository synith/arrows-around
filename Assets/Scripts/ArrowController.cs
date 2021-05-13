using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private ArrowBoundary arrowBoundary;
    private ArrowCollision arrowCollision;
    public ArrowMovement arrowMovement;
    public PlayerController playerController;
    public GameManager gameManager;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        arrowBoundary = GetComponent<ArrowBoundary>();
        arrowMovement = GetComponent<ArrowMovement>();
        arrowCollision = GetComponent<ArrowCollision>();
    }
    private void FixedUpdate()
    {
        arrowBoundary.DestroyOutOfBounds();
        if (!gameManager.gameOver)
        {
            arrowMovement.MoveArrow();
        }
        else
        {
            arrowCollision.StartCoroutine("DespawnArrow");
        }
    }
}