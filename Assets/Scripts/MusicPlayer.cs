using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Game.MusicPlayer = this;
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }


}
