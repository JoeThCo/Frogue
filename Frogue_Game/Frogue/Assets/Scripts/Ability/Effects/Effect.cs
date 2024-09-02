using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{
    public Color Color;
    public Sprite Icon;
    [Space(10)]
    public ParticleSystem vfx;
    public AudioSO audioSO;
}