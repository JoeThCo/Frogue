using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trigger", menuName = "ScriptableObjects/Trigger")]
public class Trigger : ScriptableObject
{
    [SerializeField] private ParticleSystem vfx;

    public virtual bool isTriggering()
    {
        return false;
    }
}