using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>(); // Make sure this GameObject has PlayerMovement attached

        // OR if PlayerMovement is on a different GameObject:
        // playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float speed = playerMovement.GetSpeed();

        if (speed > 0.1f) // moving
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 1);
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 3);
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 4);
            else if (Input.GetKey(KeyCode.W))
                animator.SetInteger("AnimState", 2);
        }
        else
        {
            animator.SetInteger("AnimState", 0); // idle
        }
    }
}