using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip levelSelectionMusic;
    public AudioClip[] mainLevelMusic;
    public AudioClip[] eventMusic;
    public AudioClip shopMusic;

    private AudioSource source;

    public static MusicManager instance;

    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayLevelMusic()
    {
        source.clip = mainLevelMusic[Random.Range(0, mainLevelMusic.Length-1)];
        Play();
    }

    public void PlayLevelSelectionMusic()
    {
        source.clip = levelSelectionMusic;
        Play();
    }

    public void PlayEventMusic()
    {
        source.clip = eventMusic[Random.Range(0, eventMusic.Length - 1)];
        Play();
    }

    public void PlayShopMusic()
    {
        source.clip = shopMusic;
        Play();
    }

    private void Play()
    {
        source.Play();
    }
}
