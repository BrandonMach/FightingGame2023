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

    [Header("Rounds")]
    [SerializeField] private float _roundTimer = 99;
    [SerializeField] private TextMeshProUGUI _clockText;
    [SerializeField] private roundIndicatorScript _roundScript;
    [SerializeField] private Image[] _p1RoundIndicator;
    [SerializeField] private Image[] _p2RoundIndicator;


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


    void Update()
    {
        _roundTimer -= Time.deltaTime;
        _roundTimer = Mathf.Clamp(_roundTimer, 0, 99);

        _clockText.text = ((int)Mathf.Round(_roundTimer)).ToString();
        UpdateHealthBar();
        CheckNormalKO();
        
    }

    void UpdateHealthBar()
    {
        _playerHealthBars[0].value = playerArray[0].GetComponent<PlayerHP>().healthPoints;
        _hpText[0].text = "100" + " / " + _playerHealthBars[0].value;

        _playerHealthBars[1].value = playerArray[1].GetComponent<PlayerHP>().healthPoints;
        _hpText[1].text = _playerHealthBars[1].value + " / " + "100";
    }

    void CheckNormalKO()
    {
        for (int i = 0; i < _playerHealthBars.Length; i++)
        {
            if (_playerHealthBars[i].value <= 0)//If player has won 1 round already
            {
                GiveRoundPoint(i, "NormalKO");
            }

        }
    }

    void GiveRoundPoint(int playerLostIndex, string KOType)
    {

        if(playerLostIndex == 0)//Player 1 Lost
        {
            if(!playerArray[1].GetComponent<PlayerControllerTest>().round1Won)
            {
                _p2RoundIndicator[0].color = _roundScript.koType[KOType];
                ResetRound();
                playerArray[1].GetComponent<PlayerControllerTest>().round1Won = true;
            }
            else
            {
                GameSet();
            }
        }
        if(playerLostIndex == 1)//Player 2 Lost
        {
            if(!playerArray[0].GetComponent<PlayerControllerTest>().round1Won)
            {
                _p1RoundIndicator[0].color = _roundScript.koType[KOType];
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
        _roundTimer = 99;
        playerArray[0].transform.position = spawnPositionsP1;
        playerArray[1].transform.position = spawnPositionsP2;
    }

    void GameSet()
    {
        _gameSplashScreen.SetActive(true);
    }
}
