using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability/Ability")]
public class Ability : ScriptableObject
{
    [TextArea] public string abilityDescription;

    [Header("Cause")]
    public Who[] TriggerWhos;

    [Header("Effect")]    
    public Who ApplyEffectWho;
    public Effect[] EffectsToApply;

    public IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        if (IsTriggering(beingHolder))
        {
            foreach (Being being in ApplyEffectWho.GetWho(beingHolder))
            {
                foreach (Effect effect in EffectsToApply)
                {
                    being.Effects.AddEffect(effect);
                    yield return SpawnEffectVFX(being, effect);
                }
            }
        }
    }

    private bool IsTriggering(BeingHolder beingHolder)
    {
        bool triggerOutput = true;

        foreach (Who who in TriggerWhos) 
            if (!who.IsTriggering(beingHolder))
                triggerOutput = false;

        return triggerOutput;
    }

    private IEnumerator SpawnEffectVFX(Being being, Effect effect)
    {
        ParticleSystem vfx = Instantiate(ResourceManager.GetVFX(effect), being.transform.position, Quaternion.identity);
        vfx.Play();

        yield return new WaitForSeconds(vfx.main.duration);
    }
}