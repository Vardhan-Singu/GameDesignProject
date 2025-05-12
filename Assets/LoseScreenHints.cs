using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public GameObject loseScreenUI;  // Your lose screen GameObject
    public Text hintText;            // The Text UI element for hints

    string[] hints = {
        "Press R to rotate yourself",
        "Hold Shift to sprint",
        "Avoid red zones—they're dangerous!",
        "Use cover to stay hidden",
        "Some walls can be climbed!"
    };

    public void ShowLoseScreen()
    {
        loseScreenUI.SetActive(true);
        hintText.text = "Hint: " + hints[Random.Range(0, hints.Length)];
    }
}
