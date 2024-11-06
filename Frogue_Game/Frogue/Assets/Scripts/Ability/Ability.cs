using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability/Ability")]
public class Ability : ScriptableObject
{
    [Header("If...")]
    public LocationWho[] LocationWhos;
    public ConditionWho[] ConditionWhos;

    [Header("Then...")]
    public Effect[] EffectsToApply;

    public IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        foreach (BeingSlot beingslot in beingHolder.GetAliveBeingSlots())
        {
            foreach (LocationWho locationWho in LocationWhos)
            {
                foreach (ConditionWho conditionWho in ConditionWhos)
                {
                    if (!locationWho.IsInWho(beingslot) || !conditionWho.IsInWho(beingslot)) continue;

                    foreach (Effect effect in EffectsToApply.OrderBy(e => e.Priority).ThenBy(e => e.name))
                    {
                        beingslot.BeingController.Being.Effects.AddEffect(effect);
                        beingslot.BeingController.AddEffect(effect); //visual effect
                        yield return SpawnEffectVFX(beingslot.BeingController, effect);
                    }
                }
            }
        }
    }

    private IEnumerator SpawnEffectVFX(BeingController beingController, Effect effect)
    {
        ParticleSystem vfx = Instantiate(effect.vfx, beingController.transform.position, Quaternion.identity);
        vfx.Play();
        yield return new WaitForSeconds(vfx.main.duration);
    }
}