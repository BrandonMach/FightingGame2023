using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update

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
}
