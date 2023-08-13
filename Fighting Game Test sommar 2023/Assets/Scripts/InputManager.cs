using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode _forward;
    [SerializeField] private KeyCode _back;
    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _crouch;
   
    [SerializeField] private KeyCode _punch;
    [SerializeField] private KeyCode _kick;

    [SerializeField] private PlayerControllerTest _pctScript;
    [SerializeField] private Attacks _attackScript;


    [Header("HealthPoints")]
    [SerializeField] PlayerHP _playerHPScript;



    private void Start()
    {
        if (_pctScript.Player1)
        {
            _forward = KeyCode.D;
            _back = KeyCode.A;
            _jump = KeyCode.W;
            _crouch = KeyCode.S;
            _punch = KeyCode.G;
            _kick = KeyCode.H;
        }
        else 
        {
            _forward = KeyCode.RightArrow;
            _back = KeyCode.LeftArrow;
            _jump = KeyCode.UpArrow;
            _crouch = KeyCode.DownArrow;
            _punch = KeyCode.K;
            _kick = KeyCode.L;
        }
    }

    private void Update()
    {
     



        if(_playerHPScript.kbCounter <= 0)
        {
            //move
            if (Input.GetKey(_forward) && _pctScript.IsGrounded())
            {
                _pctScript.idle = false;
                _pctScript.Move(1);
            }
            if (Input.GetKey(_back) && _pctScript.IsGrounded())
            {
                _pctScript.idle = false;
                _pctScript.Move(-1);
            }


            //Jumps
            if (Input.GetKey(_forward) && _pctScript.IsGrounded())
            {
                if (Input.GetKeyDown(_jump))
                {
                    _pctScript.Jump("forward");
                }

            }
            if (Input.GetKey(_back) && _pctScript.IsGrounded())
            {
                if (Input.GetKeyDown(_jump))
                {
                    _pctScript.Jump("back");
                }
            }

            else if (Input.GetKeyDown(_jump) && _pctScript.IsGrounded())
            {

                _pctScript.Jump("neutral");
            }

            //Crouching

            if (Input.GetKey(_crouch))
            {
                _pctScript.Crouch(true);
            }
            else
            {
                _pctScript.Crouch(false);
            }


            //Punch

            if (Input.GetKeyDown(_punch))
            {
                _attackScript.Punch();
            }


            //Kick

            if (Input.GetKeyDown(_kick))
            {
                _attackScript.HighKick();
            }
        }
        else
        {
            _pctScript.KnockBack(_playerHPScript.kbForce);
            _playerHPScript.kbCounter -= Time.deltaTime;
        }
        
        
    }
}
