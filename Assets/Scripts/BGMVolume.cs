using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolume : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource bgmAudio;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bgmAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        bgmAudio.volume = 0.04f * gameManager.volume;
    }
}
