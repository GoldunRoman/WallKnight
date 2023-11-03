using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Properties
    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _playerCollider;


    //Fields
    [SerializeField]
    private float _velocityKof;
    [SerializeField]
    private float _jumpForceKof;
    [SerializeField]
    private float _wallJumpKof;
    [SerializeField]
    private float _overlapRadiusKof = 1f;

    private float _maxSpeed = 10f;
    private Vector2 _currentVelocity;
    [SerializeField]
    private float _smoothTime = 0.2f;

    private bool _isGrounded = false;
    private bool _isWallTouch = false;



    public void Initialize()
    {
        _playerTransform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void CollisionCheck()
    {
        bool isGrounded = false;
        bool isWallTouch = false;

        float radius = _overlapRadiusKof;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, radius);

        int groundLayerIndex = 6;
        int wallLayerIndex = 7;
        foreach (Collider2D col in collider)
        {
            if (col.gameObject.layer == groundLayerIndex)
            {
                isGrounded = true;
            }
            if (col.gameObject.layer == wallLayerIndex)
            {
                isWallTouch = true;
            }
        }

        _isGrounded = isGrounded;
        _isWallTouch = isWallTouch;
    }


    private void Run()
    {
        float moveInput = Input.GetAxis("Horizontal");

        Vector2 velocity = _rb.velocity;
        float verticalVelocity = velocity.y;

        float targetHorizontalVelocity = Mathf.Clamp(moveInput * _velocityKof, -_maxSpeed, _maxSpeed);

        _rb.velocity = new Vector2(targetHorizontalVelocity, verticalVelocity);
    }


    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForceKof, ForceMode2D.Impulse);


        if (_isWallTouch == true)
        {
            _rb.AddForce(transform.right * Input.GetAxis("Horizontal") * -1 * (_jumpForceKof * 2), ForceMode2D.Impulse);
        }
    }


    #region Monobehaviour
    private void Update()
    {
        CollisionCheck();
        Debug.Log($"Velocity = {_rb.velocity}");
        Debug.Log($"IsGrounded = {_isGrounded}");
        


        if (Input.GetButton("Horizontal"))
            Run();



        if (_isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        else if(_isWallTouch && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    #endregion
}
