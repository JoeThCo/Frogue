using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SoundMixSO", fileName = "New SoundMixSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip Clip;
    [Space(10)]

    [Range(.1f, 1f)]
    [SerializeField] public float Volume = 1;

    [SerializeField][Range(-.25f, 0f)] private float frequencyMin = 0;
    [SerializeField][Range(0f, .25f)] private float frequencyMax = 0;

    public float GetRandomFrequency()
    {
        return 1 + Random.Range(frequencyMin, frequencyMax);
    }
}