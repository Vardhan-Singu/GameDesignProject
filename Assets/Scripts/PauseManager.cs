using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Store current scene name in PlayerPrefs
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("PauseMenu");
        }
    }
}