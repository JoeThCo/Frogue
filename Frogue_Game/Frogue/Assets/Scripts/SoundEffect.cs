using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioScriptableObject audioSO)
    {
        audioSource.clip = audioSO.Clip;

        audioSource.pitch = audioSO.GetRandomFrequency();
        audioSource.volume = audioSO.Volume;

        audioSource.Play();
        Destroy(gameObject, audioSO.Clip.length);
    }
}