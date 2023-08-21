using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[System.Serializable]
public class GameManager : MonoBehaviour
{
   [Header("PlayerSpawn")]
    private int spawnXAxel = 6;
    private float spawnYAxel = -2.5f;
    private Vector2 spawnPositionsP1;
    private Vector2 spawnPositionsP2; 

    [SerializeField] public static GameObject[] playerArray;




    [Header("HealthPoints")]
    [SerializeField] private  Slider[] _playerHealthBars;
    [SerializeField] private TextMeshProUGUI[] _hpText;

    [Header("GameSplashArt")]
    [SerializeField] private GameObject _gameSplashScreen;


    void Start()
    {
        playerArray = GameObject.FindGameObjectsWithTag("Player");

        //Player Spawn Pos
        spawnPositionsP1 = new Vector2(-spawnXAxel, spawnYAxel);
        spawnPositionsP2 = new Vector2(spawnXAxel, spawnYAxel);

        playerArray[0].transform.position = spawnPositionsP1;
        playerArray[0].GetComponent<PlayerControllerTest>().Player1 = true;
        playerArray[1].transform.position = spawnPositionsP2;


        int playerNumberAssign = 1;
        
        foreach (GameObject player in playerArray)
        {
            player.name = "Player: " + playerNumberAssign.ToString();
            playerNumberAssign++;
        }

        _playerHealthBars[0].value = playerArray[0].GetComponent<PlayerHP>().healthPoints;
        _playerHealthBars[1].value = playerArray[1].GetComponent<PlayerHP>().healthPoints;

        _gameSplashScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Kolla vilken sida texten ska vara
        _playerHealthBars[0].value = playerArray[0].GetComponent<PlayerHP>().healthPoints;
        _hpText[0].text = "100" + " / " + _playerHealthBars[0].value;

        _playerHealthBars[1].value = playerArray[1].GetComponent<PlayerHP>().healthPoints;
        _hpText[1].text = _playerHealthBars[1].value + " / " + "100";

        for (int i = 0; i < _playerHealthBars.Length; i++)
        {
            if (_playerHealthBars[i].value <= 0 )//If player has won 1 round already
            {
                GiveRoundPoint(i);

                
            }

        }
        
    }

    void GiveRoundPoint(int playerLostIndex)
    {

        if(playerLostIndex == 0)
        {
            if(!playerArray[1].GetComponent<PlayerControllerTest>().round1Won)
            {
                ResetRound();
                playerArray[1].GetComponent<PlayerControllerTest>().round1Won = true;
            }
            else
            {
                GameSet();
            }
        }
        if(playerLostIndex == 1)
        {
            if(!playerArray[0].GetComponent<PlayerControllerTest>().round1Won)
            {
                ResetRound();
                playerArray[0].GetComponent<PlayerControllerTest>().round1Won = true;
            }
            else
            {
                GameSet();
            }
        }

       
    }

    private void ResetRound()
    {
        //Player Spawn Pos
        foreach (GameObject player in playerArray)
        {
            player.GetComponent<PlayerHP>().ResetHP();
        }

        playerArray[0].transform.position = spawnPositionsP1;
        playerArray[1].transform.position = spawnPositionsP2;
    }

    void GameSet()
    {
        _gameSplashScreen.SetActive(true);
    }
}
