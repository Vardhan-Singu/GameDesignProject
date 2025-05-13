using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWallColliderCaveFinish : MonoBehaviour 
{
    public LevelManager levelManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        levelManager.LoadLevel("Credits");
    }
}
