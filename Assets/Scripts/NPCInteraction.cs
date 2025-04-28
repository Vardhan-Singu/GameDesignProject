using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject popupPanel;

    void Start()
    {
        popupPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        popupPanel.SetActive(true);
    }
}