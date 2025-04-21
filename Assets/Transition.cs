using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    Animator animator;
    bool isSkateboarding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Toggle skateboarding on/off with Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSkateboarding = !isSkateboarding;
        }

        // Skateboarding animations
        if (isSkateboarding)
        {
            animator.SetInteger("AnimState", 5); // Skateboard animation state
            return; // Don't check other movement
        }

        // Normal movement animations
        bool pressingD = Input.GetKey(KeyCode.D);
        bool pressingA = Input.GetKey(KeyCode.A);
        bool pressingW = Input.GetKey(KeyCode.W);
        bool pressingShift = Input.GetKey(KeyCode.LeftShift);

        if (pressingD && pressingShift)
        {
            animator.SetInteger("AnimState", 1); // Run Right
        }
        else if (pressingA && pressingShift)
        {
            animator.SetInteger("AnimState", 3); // Run Left
        }
        else if (pressingW && pressingA)
        {
            animator.SetInteger("AnimState", 4); // Diagonal Up-Left
        }
        else if (pressingW)
        {
            animator.SetInteger("AnimState", 2); // Walk Up
        }
        else
        {
            animator.SetInteger("AnimState", 0); // Idle
        }
    }
}