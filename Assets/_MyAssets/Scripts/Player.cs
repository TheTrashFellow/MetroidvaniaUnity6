using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float _speedPlayer = 1f;
    [SerializeField] public int _jumpPower = 800;
    [SerializeField] private GameObject _controllerObject = default;
    [SerializeField] public Animator _animator = default;
    private PlayerControler _controller;    
    internal Rigidbody2D _rigidBody;

    public bool _isWalking = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _controller = _controllerObject.GetComponent<PlayerControler>();
        _controller._parentPlayer = this;        
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _controller.totalJumpTokens = 0;
        }
    }
}
