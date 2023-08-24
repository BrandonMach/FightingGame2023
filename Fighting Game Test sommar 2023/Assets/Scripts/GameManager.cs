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
    private int player1 = 0;
    private int player2 = 1;



    [Header("HealthPoints")]
    [SerializeField] private  Slider[] _playerHealthBars;
    [SerializeField] private TextMeshProUGUI[] _hpText;

    [Header("Timer")]
    [SerializeField] private float _roundTimer = 99;
    [SerializeField] private TextMeshProUGUI _clockText;
    

    [Header("Rounds")]
    [SerializeField] private roundIndicatorScript _roundScript;
    [SerializeField] private Image[] _p1RoundIndicator;
    [SerializeField] private Image[] _p2RoundIndicator;
    bool _roundsOver = false;


    [Header("KOSplashArt")]
    [SerializeField] private GameObject _gameSplashScreen;
    [SerializeField] private GameObject _perfectSplashScreen;
    [SerializeField] private GameObject _timeSplashScreen;
    [SerializeField] private GameObject _koSplashScreen;


    void Start()
    {
        playerArray = GameObject.FindGameObjectsWithTag("Player");

        //Player Spawn Pos
        spawnPositionsP1 = new Vector2(-spawnXAxel, spawnYAxel);
        spawnPositionsP2 = new Vector2(spawnXAxel, spawnYAxel);

        playerArray[player1].transform.position = spawnPositionsP1;
        playerArray[player1].GetComponent<PlayerControllerTest>().Player1 = true;
        playerArray[player2].transform.position = spawnPositionsP2;


        int playerNumberAssign = 1;
        
        foreach (GameObject player in playerArray)
        {
            player.name = "Player: " + playerNumberAssign.ToString();
            playerNumberAssign++;
        }

        _playerHealthBars[player1].value = playerArray[player1].GetComponent<PlayerHP>().healthPoints;
        _playerHealthBars[player2].value = playerArray[player2].GetComponent<PlayerHP>().healthPoints;


        //SplashScreen
        _gameSplashScreen.SetActive(false);
        _perfectSplashScreen.SetActive(false);
        _timeSplashScreen.SetActive(false);
        _koSplashScreen.SetActive(false);
    }


    void Update()
    {
        _roundTimer -= Time.deltaTime;
        _roundTimer = Mathf.Clamp(_roundTimer, 0, 99);

        _clockText.text = ((int)Mathf.Round(_roundTimer)).ToString();

        UpdateHealthBars();
        CheckTimeKO();
        CheckNormalKO();
        
    }

    void CheckTimeKO()
    {
        if (_roundTimer <= 0)
        {
            _timeSplashScreen.SetActive(true);

            if (_playerHealthBars[player1].value == _playerHealthBars[player2].value)
            {
                Debug.LogError("Timeout TIE!!!");
            }
            else if (_playerHealthBars[player1].value > _playerHealthBars[player2].value) //Player 1 more HP
            {
                GiveRoundPoint(1, "TimeKO");
            }
            else
            {
                GiveRoundPoint(0, "TimeKO");
            }
        }
    }

    void UpdateHealthBars()
    {
        _playerHealthBars[player1].value = playerArray[player1].GetComponent<PlayerHP>().healthPoints;
        _hpText[player1].text = "100" + " / " + _playerHealthBars[player1].value;

        _playerHealthBars[player2].value = playerArray[player2].GetComponent<PlayerHP>().healthPoints;
        _hpText[player2].text = _playerHealthBars[player2].value + " / " + "100";
    }

    void CheckNormalKO()
    {
        for (int i = 0; i < _playerHealthBars.Length; i++)
        {
            if (_playerHealthBars[i].value <= 0 && !_roundsOver)//If player has won 1 round already
            {
                _roundsOver = true;

                GiveRoundPoint(i, "NormalKO");
            }

        }
    }

    void GiveRoundPoint(int playerLostIndex, string KOType) 
    {
        if(playerLostIndex == player1)//Player 1 Lost
        {
            int player2RoundsWon = playerArray[player2].GetComponent<PlayerControllerTest>().roundsWon;

            _p2RoundIndicator[player2RoundsWon].color = _roundScript.koType[CheckPerfectKO(_playerHealthBars[player2].value)]; //Check Player 2 HP
            playerArray[player2].GetComponent<PlayerControllerTest>().roundsWon++; //Give Player 2 a round point

        }
        if(playerLostIndex == player2)//Player 2 Lost
        {
            int player1RoundsWon = playerArray[player1].GetComponent<PlayerControllerTest>().roundsWon;

            _p1RoundIndicator[player1RoundsWon].color = _roundScript.koType[CheckPerfectKO(_playerHealthBars[player1].value)];//Check Player 1 HP 
            playerArray[player1].GetComponent<PlayerControllerTest>().roundsWon++; //Give Player 2 a round point
        }

        StartCoroutine(WaitRestartRound());

        //if (playerLostIndex == 0 && playerArray[1].GetComponent<PlayerControllerTest>().round1Won || playerLostIndex == 1 && playerArray[0].GetComponent<PlayerControllerTest>().round1Won)
        //{
        //    GameSet();
        //}
    }
    /// <summary>
    /// int playerLostIndex = Player that lost the round, string KOType = The KO type
    /// </summary>
   
    string CheckPerfectKO(float winnerHP)
    {
        if (winnerHP == 100)
        {
           _perfectSplashScreen.SetActive(true);
           return "PerfectKO";
        }
        else
        {
            _koSplashScreen.SetActive(true);
            return "NormalKO";
        }
    }

    private IEnumerator WaitRestartRound()
    {
        yield return new WaitForSeconds(5);
        ResetRound();
    }

    private void ResetRound()
    {
        //Player Spawn Pos
        foreach (GameObject player in playerArray)
        {
            player.GetComponent<PlayerHP>().ResetHP();
        }
        _koSplashScreen.SetActive(false);
        _perfectSplashScreen.SetActive(false);
        _timeSplashScreen.SetActive(false);
        _roundTimer = 99;
        playerArray[player1].transform.position = spawnPositionsP1;
        playerArray[player2].transform.position = spawnPositionsP2;
        _roundsOver = false;
    }

    void GameSet()
    {
        _gameSplashScreen.SetActive(true);
    }
}
