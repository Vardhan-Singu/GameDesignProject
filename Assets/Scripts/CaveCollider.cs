using UnityEngine;

public class CaveCollider : MonoBehaviour
{
    public LevelManager levelManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        levelManager.LoadLevel("Lose #2");
    }
}
