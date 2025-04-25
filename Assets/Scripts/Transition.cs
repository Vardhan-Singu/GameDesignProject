using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator animator;
    public Movement Movement;
    private bool isSkateboarding = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // If not found on this object, try to find it in parent
        if (rb == null)
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }

        // If still not found, log and disable
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name + " or its parents. Please assign it.");
            enabled = false;
        }
        
        if (animator == null)
            animator = GetComponent<Animator>();

        if (Movement == null)
        {
            Movement = GetComponent<Movement>();
            if (Movement == null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                    Movement = player.GetComponent<Movement>();
            }
        }
    }

    void Update()
    {
        // Stop if animator or Movement is still not found
        if (animator == null || Movement == null)
        {
            Debug.LogWarning("Animator or Movement component is missing on: " + gameObject.name);
            return;
        }

        // Toggle skateboarding mode
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSkateboarding = !isSkateboarding;
            Debug.Log("Skateboarding mode: " + isSkateboarding);
        }

        // Get speed from Movement
        float speed = Movement.GetSpeed();
        Debug.Log("Current Speed: " + speed); // Debug the speed value

        // Determine animation state based on speed
        if (speed > 0.07f)  // Threshold for walking/running
        {
            bool pressingD = Input.GetKey(KeyCode.D);
            bool pressingA = Input.GetKey(KeyCode.A);
            bool pressingShift = Input.GetKey(KeyCode.LeftShift);

            if (pressingD && pressingShift)
            {
                animator.SetInteger("AnimState", 11); // Run Right
            }
            else if (pressingA && pressingShift)
            {
                animator.SetInteger("AnimState", 12); // Run Left
            }
            else if (!Movement.isGrounded && pressingD)
            {
                animator.SetInteger("AnimState", 13); // Falling right
            }
            else if (!Movement.isGrounded && pressingA)
            {
                animator.SetInteger("AnimState", 14); // Falling left
            }
        }
        else
        {
            animator.SetInteger("AnimState", 10); // Idle
        }
    }
}
