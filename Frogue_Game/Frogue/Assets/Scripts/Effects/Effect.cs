using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect")]
public class Effect : ScriptableObject
{
    [SerializeField] private ParticleSystem vfx;

    public virtual void ApplyEffect()
    {

    }
}