using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Ability
{
    public Trigger Trigger { get; private set; }
    public Effect Effect { get; private set; }

    public Ability()
    {
        Trigger = ResourceManager.GetTrigger();
        Trigger.TriggerInit();

        Effect = ResourceManager.GetEffect();
        Effect.EffectInit();

        //Debug.LogFormat("{0} {1}", Trigger.ToString(), Effect.ToString());
    }

    public IEnumerator TryApplyEffect()
    {
        if (Trigger.isTriggering())
        {
            yield return Effect.ApplyEffect();
        }
    }
}