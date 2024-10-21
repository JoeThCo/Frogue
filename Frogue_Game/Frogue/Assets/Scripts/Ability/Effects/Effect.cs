using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{
    public Color Color;
    public Sprite Icon;
    [Range(1, 10)] public int Priority = 1;
    [Space(10)]
    public ParticleSystem vfx;
    public AudioSO audioSO;
}