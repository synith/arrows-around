using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerHp = 10;
    public int shieldHp = 5;
    public bool gameOver;
    public bool gameStarted;
    public bool shieldBroken;
    public int playerMaxhp = 10;
    public int shieldMaxhp = 5;

    [SerializeField] TextMeshProUGUI playerhpText;
    [SerializeField] TextMeshProUGUI shieldhpText;
    [SerializeField] TextMeshProUGUI gameoverText;

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

    // Update is called once per frame
    void Update()
    {
        playerHp = playerMaxhp - playerController.bodyHits;
        shieldHp = shieldMaxhp - playerController.shieldHits;

        playerhpText.text = "HP: " + playerHp;
        shieldhpText.text = "Shield HP: " + shieldHp;

        if (playerHp < 1)
        {
            gameOver = true;
            playerController.isDead = true;
            restartButton.gameObject.SetActive(true);
            gameoverText.gameObject.SetActive(true);
            playerHp = 0;
        }

        if (shieldHp <= 0)
            shieldBroken = true;

        if (shieldHp > 0)
            shieldBroken = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
