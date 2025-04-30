using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    public PlayerMovement playerMovement;

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

        if (playerMovement == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
            Debug.Log("On Skateboard: " + playerController.isOnSkateboard);
            if (animator == null || playerController == null || playerMovement == null)
                return;
            bool isSkating = playerMovement.enabled;
            if (isSkating)
                HandleSkateboardingAnimation();
            else
                HandleWalkingAnimation();
    }

    void HandleWalkingAnimation()
    {
        if (!playerController.CheckIfGrounded())
        {
            if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 4); // Fall right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 5); // Fall left
            else
                animator.SetInteger("AnimState", 4); // Default fall right
            return;
        }

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

    void HandleSkateboardingAnimation()
    {
        float speed = playerMovement.GetSpeed();

        if (!playerMovement.isGrounded)
        {
            animator.SetInteger("AnimState", 7); // In air
            return;
        }

        if (speed > 0.07f)
        {
            float yRotation = transform.rotation.eulerAngles.y;
            
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 1); // Boost right
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("AnimState", 3); // Boost left
            else if (Input.GetKey(KeyCode.D))
                animator.SetInteger("AnimState", 5); // Skate right
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("AnimState", 6); // Skate left
            else if (Input.GetKey(KeyCode.S))
            {
                if (yRotation == 0f)
                    animator.SetInteger("AnimState", 8); // Power slide right
                else if (yRotation == 180f)
                    animator.SetInteger("AnimState", 9); // Power slide left
            }
        }
        else
        {
            animator.SetInteger("AnimState", 0); // Idle
        }
    }
}
