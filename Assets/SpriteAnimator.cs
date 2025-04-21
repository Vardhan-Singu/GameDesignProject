using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Animator animator; // Drag in inspector if needed
    public PlayerMovement playerMovement; // Drag the player object with PlayerMovement here

    void Start()
    {
        // Fallback if animator wasn't assigned in Inspector
        if (animator == null)
            animator = GetComponent<Animator>();

        // Try get PlayerMovement from same object
        playerMovement = GetComponent<PlayerMovement>();

        // If still null, try find it elsewhere
        if (playerMovement == null)
        {
            GameObject player = GameObject.FindWithTag("Player"); // <-- Make sure your player has this tag
            if (player != null)
                playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (animator == null || playerMovement == null)
        {
            Debug.LogWarning("Animator or PlayerMovement is not assigned!");
            return;
        }

        if (!playerMovement.isOnSkateboard)
        {
            animator.SetInteger("AnimState", 0);
            return;
        }

        float speed = playerMovement.GetSpeed();

        if (!playerMovement.isGrounded)
        {
            animator.SetInteger("AnimState", 7); // air
        }
        else if (speed > 0.07f)
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 1); // boost right
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 3); // boost left
            else if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 5); // skate right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 6); // skate left
            else if (Input.GetKey(KeyCode.W))
                animator.SetInteger("AnimState", 2); // up movement
        }
        else
        {
            animator.SetInteger("AnimState", 0); // idle
        }
    }
}

