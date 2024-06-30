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

    public IEnumerator TryApplyEffect(BattleState battleState)
    {
        if (Trigger.isTriggering(battleState))
        {
            yield return Trigger.OnTriggered(battleState);
            yield return Effect.ApplyEffect(battleState);
        }
    }
}