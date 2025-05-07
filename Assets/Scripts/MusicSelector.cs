using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicSelector : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;
    public string[] songNames;
    public TMP_Dropdown dropdown;
    public TMP_Text songDisplay;

    void Start()
    {
        // Fill dropdown with song names
        dropdown.ClearOptions();
        dropdown.AddOptions(new System.Collections.Generic.List<string>(songNames));
        dropdown.onValueChanged.AddListener(PlaySelectedSong);
        
        // Play default song
        PlaySelectedSong(0);
    }

    void PlaySelectedSong(int index)
    {
        audioSource.clip = songs[index];
        audioSource.Play();
        songDisplay.text = "Now Playing: " + songNames[index];
    }
}
