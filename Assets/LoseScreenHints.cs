using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Text hintText; // Assign this in the Inspector
    private string[] hints = {
        "Spam the shift bar as fast as you can to speed up.",
        "Use the minimap to find any upcoming obstacles or where you're about to land.",
        "Use 'Alt' to slow down and pair it with the 'Q' and 'E' to land perfectly.",
        "Use R to rotate yourself.",
        "Use 'S' to power slide",
        "I wonder what spamming tab does..."
    };

    void OnEnable()
    {
        // Show a random hint when the lose screen is activated
        int index = Random.Range(0, hints.Length);
        hintText.text = "Hint: " + hints[index];
    }
}
