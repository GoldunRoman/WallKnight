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
    private float _velocity;
    [SerializeField]
    private float _jumpForce;
    private bool _isGrounded;

    public void Initialize()
    {
        _playerTransform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string layer = collision.gameObject.layer.ToString();
        _isGrounded = (layer == "Ground");
        Debug.Log(_isGrounded);
    }

    private void IsGrounded()
    {
    }

    private void Jump()
    {
        if (!_isGrounded) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce((Vector2.up * _jumpForce) * Time.deltaTime);
        }
    }

    #region Monobehaviour
    private void Update()
    {
        Jump();
    }

    #endregion
}
