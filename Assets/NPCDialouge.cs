using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class NPCDialouge : MonoBehaviour
{
    public GameObject dialogueBubble; // Reference to the dialogue UI
    public TextMeshProUGUI dialogueText; // Text component inside the dialogue bubble
    public GameObject interactMessage; // "Press E to interact" message

    public string[] dialogueLines; // Array of dialogue lines
    private int currentLine = 0;
    private bool isPlayerNear = false;
    private bool isDialogueActive = false; // Track if dialogue is active

    void Start()
    {
        dialogueBubble.SetActive(false); // Hide dialogue at the start
        interactMessage.SetActive(false); // Hide interact message at the start
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                ShowDialogue();
            }
            else
            {
                NextDialogueLine();
            }
        }
    }

    private void ShowDialogue()
    {
        isDialogueActive = true;
        dialogueBubble.SetActive(true);
        interactMessage.SetActive(false); // Hide "Press E" message
        dialogueText.text = dialogueLines[currentLine];
    }

    private void NextDialogueLine()
    {
        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            dialogueBubble.SetActive(false); // Hide bubble when finished
            interactMessage.SetActive(true); // Show "Press E" again
            isDialogueActive = false;
            currentLine = 0; // Reset dialogue
        }
        else
        {
            dialogueText.text = dialogueLines[currentLine]; // Show next line
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactMessage.SetActive(true); // Show "Press E to interact"
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactMessage.SetActive(false); // Hide "Press E"
            dialogueBubble.SetActive(false); // Hide dialogue
            isDialogueActive = false;
            currentLine = 0; // Reset dialogue
        }
    }
}
