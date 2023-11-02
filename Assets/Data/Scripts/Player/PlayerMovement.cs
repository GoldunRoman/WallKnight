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
    private float _overlapRadiusKof = 1f;

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
            isGrounded = (col.gameObject.layer == groundLayerIndex);
            isWallTouch = (col.gameObject.layer == wallLayerIndex);
        }

        _isGrounded = isGrounded;
        _isWallTouch = isWallTouch;
    }


    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * _velocityKof);
    }

    private void Jump()
    {
        if(_isWallTouch == false)
        {
            _rb.AddForce(transform.up * _jumpForceKof, ForceMode2D.Impulse);
        }

        else if (_isWallTouch == true)
        {
            //_rb.AddForce(transform)
        }
    }

    #region Monobehaviour
    private void Update()
    {
        CollisionCheck();

        if (Input.GetButton("Horizontal"))
            Run();

        if (_isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    #endregion
}
