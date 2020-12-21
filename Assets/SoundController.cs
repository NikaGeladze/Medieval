using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource loopAudioSource;

    public AudioClip testSound;
    public AudioClip gameOverSound;
    public AudioClip gameWinSound;
    public AudioClip backgroundMusic;


    private void Start() {
        loopAudioSource.clip = backgroundMusic;
        loopAudioSource.Play();
    }




    public void PlaySound(AudioClip soundClip) {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
