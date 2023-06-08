using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _playernNum;
    [SerializeField] public float _horizontal;
    [SerializeField] private float _groundSpeed = 6f;
    [SerializeField] private float _airSpeed = 4f;
    [SerializeField] private float _jumpingPower = 16f;

    [SerializeField] private Transform _stageCenter;
    [SerializeField] private bool _frontIsRight = true;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    
    [Range(0,5)]
    [SerializeField] float _groundCheckRange;

    public bool isOnGround;

    public bool idle;

    [SerializeField] bool _isCrouching;

    [Header("Attacks")]
    [SerializeField] private GameObject _kickHitBox;


 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ( transform.position.x < _stageCenter.position.x) //Inte helt rätt man ska bara flippa när andra spelaren är bakom en
        {
            _frontIsRight = true;
            Flip(_frontIsRight);
        }
        else
        {
            _frontIsRight = false;
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

    public void Crouch(bool crouch)
    {
        _isCrouching = crouch;
        _anim.SetBool("Crouching", crouch);
        
    }


    //Kick
    public void HighKick()
    {
        _anim.SetTrigger("High_Kick");
       
    }
    public void EnableKick()
    {
        _kickHitBox.SetActive(true);
    }
    public void DisableKick()
    {
        Debug.LogError("hello");
        _kickHitBox.SetActive(false);
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


    public void KnockBack( float KBForce)
    {
        if (_frontIsRight)
        {
            _rb.velocity = new Vector2(-KBForce, KBForce);
        }
        else
        {
            _rb.velocity = new Vector2(KBForce, KBForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(_groundCheck.transform.position.x, _groundCheck.transform.position.y,0), _groundCheckRange);
        
    }
}
