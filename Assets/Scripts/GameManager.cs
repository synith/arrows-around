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

    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject titleScreen;
    
    [SerializeField]
    private Slider volumeSlider;

    public float volume;

    // gain access to player script for shield and body hits
    private PlayerController playerController;
    private SpawnManager spawnManager;


    private AudioSource buttonAudio;
    [SerializeField] private AudioClip buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        buttonAudio = GetComponent<AudioSource>();

        gameOver = false;
        gameStarted = false;
    }

    private void Update()
    {
        volume = volumeSlider.value;
    }

    public void StartGame()
    {
        spawnManager.SpawnManagerStart();
        playerController.PlayerControllerStart();
        titleScreen.SetActive(false);
        gameStarted = true;
        buttonAudio.PlayOneShot(buttonClick, 0.3f * volume);
        TimerController.instance.BeginTimer();
    }
    public void RestartGame()
    {
        buttonAudio.PlayOneShot(buttonClick, 0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
