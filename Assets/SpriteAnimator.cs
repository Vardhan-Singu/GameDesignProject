using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed > 0.1f) // moving
        {
            // Optional: use direction or inputs to pick animation states
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 1);
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 3);
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 4);
            else if (Input.GetKey(KeyCode.W))
                animator.SetInteger("AnimState", 2);
        }
        else // standing still
        {
            animator.SetInteger("AnimState", 0); // idle
        }
    }
}
