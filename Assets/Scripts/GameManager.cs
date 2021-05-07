using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    // int hp
    // bool gameover

    // hp = max hp - body hits

    // if hp =< 0
    // player dies
    // game is over

    // Variables
    // int shieldHP
    // bool shieldbroken

    // shieldhp = shieldmax hp - shield hits

    // if shieldhp =< 0
    // shield broken
    // shield cant be used

    public int playerHp;
    public int shieldHp;
    public bool gameOver;
    public bool shieldBroken;
    [SerializeField] private int playerMaxhp = 10;
    [SerializeField] private int shieldMaxhp = 5;

    [SerializeField] TextMeshProUGUI playerhpText;
    [SerializeField] TextMeshProUGUI shieldhpText;
    [SerializeField] TextMeshProUGUI gameoverText;

    // gain access to player script for shield and body hits
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // find player controller script and assign it to our variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHp = playerMaxhp - playerController.bodyHits;
        shieldHp = shieldMaxhp - playerController.shieldHits;

        playerhpText.text = "HP: " + playerHp;
        shieldhpText.text = "Shield HP: " + shieldHp;       

        if (playerHp <= 0)
        {
            gameOver = true;
            gameoverText.gameObject.SetActive(true);
        }
            

        if (shieldHp <= 0)        
            shieldBroken = true;

        if (shieldHp > 0)
            shieldBroken = false;

        // check for shieldBroken in PlayerController script when spawning shield on spacebar
        // check for gameOver in other scripts before enabling movement, spawning, animations
        // if gameover load Restart screen
    }
}
