using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    float moveDirection = 0;
    bool isGrounded = false;

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

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1;
        }
        else if (isGrounded || r2d.linearVelocity.magnitude < 0.01f)
        {
            moveDirection = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.linearVelocity = new Vector2(r2d.linearVelocity.x, jumpHeight);
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);

        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);

        isGrounded = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != mainCollider)
            {
                isGrounded = true;
                Debug.Log("Landed on: " + colliders[i]);
                break;
            }
        }

        r2d.linearVelocity = new Vector2(moveDirection * maxSpeed, r2d.linearVelocity.y);
    }
}
