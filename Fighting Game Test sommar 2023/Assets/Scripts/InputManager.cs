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




    private void Start()
    {
        
    }

    private void Update()
    {
     
        //move
        if (Input.GetKey(_forward) && _pctScript.IsGrounded())
        {
            _pctScript.idle =false;
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

        //Kick

        if (Input.GetKeyDown(_kick))
        {
            _pctScript.HighKick();
        }
        
    }
}
