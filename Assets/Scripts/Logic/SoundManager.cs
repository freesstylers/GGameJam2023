using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource, sfxSource;

    public static SoundManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound (AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }
}
