using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger soundmananger;
    public AudioSource audioSource;
    public AudioClip jumpAudio, hurtAudio, cherryAudio;


    public void Awake()
    {
        soundmananger = this;
    }
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
}
