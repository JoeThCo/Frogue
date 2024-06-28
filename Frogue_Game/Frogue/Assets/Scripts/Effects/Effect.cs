using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    [SerializeField] private ParticleSystem vfx;

    public virtual void ApplyEffect()
    {

    }
}