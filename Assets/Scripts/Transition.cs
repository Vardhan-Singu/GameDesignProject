using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController; // <-- Change this

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // Try to get PlayerController
        playerController = GetComponent<PlayerController>();

        if (playerController == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (animator == null || playerController == null)
        {
            Debug.LogWarning("Animator or PlayerController is not assigned!");
            return;
        }

        // Check falling first
        if (!playerController.CheckIfGrounded())
        {
            if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 4); // fall right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 5); // fall left
            return;
        }

        float speed = Mathf.Abs(playerController.GetComponent<Rigidbody2D>().linearVelocity.x);

        if (speed > 0.07f)
        {
            if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 1); // walk right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 3); // walk left
        }
        else
        {
            animator.SetInteger("AnimState", 0); // idle
        }
    }
}
