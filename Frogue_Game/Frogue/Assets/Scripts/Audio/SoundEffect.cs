using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioSO audioSO)
    {
        audioSource.clip = audioSO.Clip;

        audioSource.pitch = audioSO.GetRandomFrequency();
        audioSource.volume = audioSO.Volume;

        audioSource.Play();
        Destroy(gameObject, audioSO.Clip.length);
    }
}