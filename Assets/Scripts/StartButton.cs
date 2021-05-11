using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPushed);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void ButtonPushed()
    {
        gameManager.StartGame();
    }
}
