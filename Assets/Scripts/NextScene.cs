using UnityEngine;

public class NextScene : MonoBehaviour
{
    public LevelManager levelManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        levelManager.LoadLevel("Level 1");
    }
}
