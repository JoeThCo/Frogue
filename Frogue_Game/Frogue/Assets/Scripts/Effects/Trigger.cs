using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trigger", menuName = "ScriptableObjects/Trigger")]
public class Trigger : ScriptableObject
{
    [SerializeField] private ParticleSystem vfx;
    public Who Who { get; private set; }

    public virtual void TriggerInit()
    {
        Who = ResourceManager.GetWho();
    }

    public virtual bool isTriggering()
    {
        return false;
    }

    public virtual IEnumerator OnTriggered()
    {
        if (vfx != null)
        {
            vfx.Play();
            yield return vfx.totalTime;
        }
    }

    public override string ToString()
    {
        return name;
    }
}