using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [SerializeField] private int _playernNum;
    [SerializeField] public float _horizontal;
    [SerializeField] private float _groundSpeed = 6f;
    [SerializeField] private float _airSpeed = 4f;
    [SerializeField] private float _jumpingPower = 16f;

    [SerializeField] private Transform _stageCenter;
    [SerializeField] private bool _isFacingRight = true;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    public bool idle;
    
    [Range(0,5)]
    [SerializeField] float _groundCheckRange;

    public bool isOnGround;

    //private enum JumpDir
    //{
    //    up,
    //    forward,
    //    back,
    //    noJump
    //}
    //JumpDir _jumpOption = JumpDir.noJump;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ( transform.position.x < _stageCenter.position.x) //Inte helt rätt man ska bara flippa när andra spelaren är bakom en
        {
            _isFacingRight = true;
            Flip(_isFacingRight);
        }
        else
        {
            _isFacingRight = false;
            Flip(false);
        }
        
        if(_playernNum == 1)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if(_playernNum ==2)
        {
            _horizontal = Input.GetAxisRaw("Horizontal_P2");
        }
       
        isOnGround = IsGrounded();
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            //Can only move front and back when on ground, jumping is a set power.
           // _rb.velocity = new Vector2(_horizontal * _groundSpeed, _rb.velocity.y);     
        }
       
        if(_rb.velocity == Vector2.zero)
        {
            idle = true;
        }
        
        
    }

    public void Move(int dir)
    {
        
        _rb.velocity = new Vector2(dir * _groundSpeed, _rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.transform.position, _groundCheckRange, _groundLayer);
    }

    public void Jump(string jumpOption)
    { 
        if(jumpOption =="neutral")
        {
            _rb.velocity = Vector2.zero;
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpingPower);
        }
        if(jumpOption == "forward")
        {
            _rb.velocity = new Vector2(_airSpeed, _jumpingPower);
        }
        if (jumpOption == "back")
        {
            _rb.velocity = new Vector2(-_airSpeed, _jumpingPower);
        }

    }

    private void Flip(bool right)
    {
        Vector3 localScale = transform.localScale;
        if (right)
        {           
            localScale.x = 1;
        }
        else
        {       
            localScale.x = -1;       
        }
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(_groundCheck.transform.position.x, _groundCheck.transform.position.y,0), _groundCheckRange);
        
    }
}
