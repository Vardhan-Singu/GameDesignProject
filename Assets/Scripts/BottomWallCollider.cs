using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWallCollider : MonoBehaviour 
{
    public LevelManager levelManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        levelManager.LoadLevel("Lose #1");
    }
}
