using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trigger
{
    [SerializeField] private ParticleSystem vfx;

    public virtual bool isTriggering()
    {
        return false;
    }
}