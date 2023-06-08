using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int _healthPoints;


    [Header("Knockback KB")]
    [SerializeField] public float kbForce;
    [SerializeField] public float kbCounter;
    [SerializeField] public float kbTotalTime;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damageAmount)
    {
        kbCounter = 0.2f;
        _healthPoints -= damageAmount;
        
    }
}
