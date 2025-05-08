using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    public void ResumeGame()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene", "GameScene"); // Default to GameScene
        SceneManager.LoadScene(previousScene);
    }
}