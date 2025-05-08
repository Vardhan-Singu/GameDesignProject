using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicDropdownController : MonoBehaviour
{
    
    public TMP_Dropdown dropdown;
    public AudioSource audioSource;
    public AudioClip[] songs;
    

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnSongSelected);
        PlaySong(0); // Play default song at start
    }

    void OnSongSelected(int index)
    {
        PlaySong(index);
    }

    void PlaySong(int index)
    {
        if (index >= 0 && index < songs.Length)
        {
            audioSource.clip = songs[index];
            audioSource.Play();
        }
    }
}
