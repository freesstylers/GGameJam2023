using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlaySound(audioClip);   
    }
}
