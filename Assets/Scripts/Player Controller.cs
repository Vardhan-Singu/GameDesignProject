using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    public Vector2 boxSize = new Vector2(0.5f, 0.1f); // renamed from groundCheckBoxSize
    public float CastDistance = 0.1f;                 // renamed from groundCheckDistance
    public LayerMask groundLayer;

    private bool isGrounded;
    public bool isOnSkateboard = false;

    float moveDirection = 0;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, CastDistance, groundLayer);
    }

    void Update()
    {
        isGrounded = CheckIfGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.linearVelocity = new Vector2(r2d.linearVelocity.x, jumpHeight);
        }
        /*
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOnSkateboard = !isOnSkateboard;
            Debug.Log("Skateboard active: " + isOnSkateboard);
        }
        */
    }

    void FixedUpdate()
    {
        // Movement input
        moveDirection = 0;
        if (Input.GetKey(KeyCode.A))
            moveDirection = -1;
        else if (Input.GetKey(KeyCode.D))
            moveDirection = 1;

        // Apply movement
        r2d.linearVelocity = new Vector2(moveDirection * maxSpeed, r2d.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance, boxSize);
    }
}