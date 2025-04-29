using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWallCollider : MonoBehaviour
{
    public LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.LoadLevel("Lose");
        }
    }
}

