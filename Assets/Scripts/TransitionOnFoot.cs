using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

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

        // ❗ FIX: Only animate when NOT on skateboard
        if (playerController.isOnSkateboard)
            return;

        // Falling
        if (!playerController.CheckIfGrounded())
        {
            if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 4); // Fall right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 5); // Fall left
            else
                animator.SetInteger("AnimState", 4); // Default fall right if no input
            return;
        }

        // Walking or Idle
        float speed = Mathf.Abs(playerController.GetComponent<Rigidbody2D>().linearVelocity.x);

        if (speed > 0.07f)
        {
            if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 1); // Walk right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 3); // Walk left
        }
        else
        {
            animator.SetInteger("AnimState", 0); // Idle
        }
    }
}
