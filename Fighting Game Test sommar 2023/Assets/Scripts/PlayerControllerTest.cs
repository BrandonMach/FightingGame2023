using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator _anim;
    

    [Header("Movement properties")]
    [SerializeField] public float _horizontal;
    [SerializeField] private float _groundSpeed = 6f;
    [SerializeField] private float _airSpeed = 4f;
    [SerializeField] private float _jumpingPower = 16f;
    [SerializeField] private BoxCollider2D _playerBoxCollider;

    [Header("Character Flip")]
    [SerializeField] private int _playernNum;
    [SerializeField] private Transform _stageCenter;
    [SerializeField] private bool _frontIsRight = true;


    [Header("Ground Check")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private BoxCollider2D hurtbox;

    [Range(0, 5)]
    [SerializeField] float _groundCheckRange;

    public bool isOnGround;

    public bool idle;

    [SerializeField] bool _isCrouching;

    [Header("Opponent")]

    [SerializeField] private GameObject _opponent;
    private PlayerControllerTest _opponentsPCT;
 

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject character in GameManager.playerArray)
        {
            if(character.gameObject != this.gameObject)
            {
                _opponent = character;
            }
        }
        _opponentsPCT = _opponent.GetComponent<PlayerControllerTest>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_isCrouching)
        {
            hurtbox.offset = new Vector2(0,-0.25f );    
            hurtbox.size = new Vector2(1.5f,2f);
        }
        else
        {
            hurtbox.offset = new Vector2(0, 0);
            hurtbox.size = new Vector2(1.5f, 2.5f);
        }

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

        // if playerNum is 1 Use Horisontal inputs for Player 1
        if(_playernNum == 1)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
        }
        // if playerNum is 2 Use Horisontal inputs for Player 2
        else if (_playernNum ==2)
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


    private void OnCollisionEnter2D(Collision2D colliderObject)
    {
        if (isOnGround)
        {
            Physics2D.IgnoreCollision(colliderObject.collider, _playerBoxCollider, false);
            Debug.LogError("Collide");
        }
        else
        {
            Physics2D.IgnoreCollision(colliderObject.collider, _playerBoxCollider);
            
        }


        //if (colliderObject.gameObject == _opponent && !isOnGround)
        //{
        //    Physics2D.IgnoreCollision(colliderObject.collider, _playerBoxCollider);
        //    Debug.LogError("Collide");   
        //}
        
    }
}
