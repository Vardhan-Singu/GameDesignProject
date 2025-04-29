using UnityEngine;
using UnityEngine.UI;

public class RandomSentenceDisplay : MonoBehaviour
{
    [TextArea]
    public string[] sentences; // Add sentences in the Inspector
    public Text displayText;   // Drag your UI Text component here

    void Start()
    {
        if (sentences.Length > 0 && displayText != null)
        {
            int randomIndex = Random.Range(0, sentences.Length);
            displayText.text = sentences[randomIndex];
        }
        else
        {
            Debug.LogWarning("Sentences or Display Text not assigned.");
        }
    }
}
