using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{    
    public Player _parentPlayer;
    public Rigidbody2D _rigidBody;

    public PlayerControls _controls;
    public InputAction _moving;
    public InputAction _jumping;
    private InputActionMap _playerActionMap;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void Start()
    {
        _rigidBody = _parentPlayer._rigidBody;        
    }

    private void Update()
    {
        PlayerMovement();
        PlayerJump();
    }

    private void OnEnable()
    {
        _moving = _controls.Player.Move;
        _moving.Enable();

        _jumping = _controls.Player.Jump;
        _jumping.Enable();
    }

    private void PlayerMovement()
    {        
        Vector2 movement = _moving.ReadValue<Vector2>();
        float x = movement.x;

        if (x != 0) 
        {
            _rigidBody.transform.Translate(new Vector3 (x * Time.deltaTime * _parentPlayer._speedPlayer, 0, 0));
            
            if(!_parentPlayer._isWalking)
            {
                _parentPlayer._animator.SetTrigger("StartWalk");
                _parentPlayer._isWalking = true;
            }
        }

        if (x == 0 && _parentPlayer._isWalking) 
        {
            _parentPlayer._animator.SetTrigger("StopWalk");
            _parentPlayer._isWalking = false;
        }
        
        
        //Pas de normalize car seulement 1 axe 

        //transform.Translate(x * Time.deltaTime * _speedPlayer
        //_rigidBody.linearVelocityX = x * Time.fixedDeltaTime * _parentPlayer._speedPlayer;
        //_rigidBody.AddForce(new Vector2(x, 0) * Time.fixedDeltaTime * _parentPlayer._speedPlayer);

    }

    private int currentJumpTokens;
    public int totalJumpTokens;
    private void PlayerJump()
    {        
        if(totalJumpTokens < 60)
        {            
            float jump = _jumping.ReadValue<float>();

            if (jump != 0)
            {                               
                currentJumpTokens++;
                totalJumpTokens++;
            }

            if (currentJumpTokens > 0)
            {                
                currentJumpTokens--;
                _rigidBody.linearVelocityY = jump * Time.fixedDeltaTime * _parentPlayer._jumpPower;                
            }            
        }        
    }
}
