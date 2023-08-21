using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    
    //Om olika karaktärer ska ha olika mycket hp
    [SerializeField] public int healthPoints;


    [Header("Knockback KB")]
    [SerializeField] public float kbForce;
    [SerializeField] public float kbCounter;
    [SerializeField] public float kbTotalTime;

    //private GameObject gM;

    void Start()
    {
        
        //gM = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damageAmount)
    {
        kbCounter = 0.2f;
        healthPoints -= damageAmount;   
    }

    public void ResetHP()
    {
        healthPoints = 100;
    }
}
