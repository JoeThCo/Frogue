using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect")]
public class Effect : ScriptableObject
{
    [SerializeField] private ParticleSystem vfx;
    public Who Who { get; private set; }

    public virtual void EffectInit()
    {
        Who = ResourceManager.GetWho();
    }

    public virtual IEnumerator ApplyEffect()
    {
        yield return vfx.totalTime;
    }

    public override string ToString()
    {
        return name;
    }
}