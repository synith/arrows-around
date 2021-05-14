using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public bool gameOver;
    public bool gameStarted;    

    public TextMeshProUGUI playerhpText;
    public TextMeshProUGUI shieldhpText;
    public TextMeshProUGUI gameoverText;

    public Button restartButton;
    public GameObject titleScreen;

    // gain access to player script for shield and body hits
    private PlayerController playerController;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        
        gameOver = false;
        gameStarted = false;
    }

    public void StartGame()
    {
        spawnManager.SpawnManagerStart();
        playerController.PlayerControllerStart();
        titleScreen.SetActive(false);
        gameStarted = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
