using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
   [Header("PlayerSpawn")]
    private int spawnXAxel = 6;
    private float spawnYAxel = -2.5f;
    private Vector2 spawnPositionsP1;
    private Vector2 spawnPositionsP2; 

    [SerializeField] public static GameObject[] playerArray;
    [SerializeField] public  Slider[] playerHealthBars;


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

      


    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
