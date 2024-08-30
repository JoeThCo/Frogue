using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability/Ability")]
public class Ability : ScriptableObject, IDescription
{
    [Header("If")]
    public LocationWho[] LocationWhos;
    public Who[] ConditionWhos;

    [Header("Then")]
    public Effect[] EffectsToApply;

    public IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        foreach (BeingSlot beingslot in beingHolder.GetAliveBeingSlots())
        {
            foreach (LocationWho locationWho in LocationWhos)
            {
                foreach (Who conditionWho in ConditionWhos)
                {
                    if (!locationWho.IsInWho(beingslot) || !conditionWho.IsInWho(beingslot)) continue;

                    foreach (Effect effect in EffectsToApply)
                    {
                        beingslot.Being.Effects.AddEffect(effect);
                        yield return SpawnEffectVFX(beingslot.Being, effect);
                    }
                }
            }
        }
    }

    public string GetDescription()
    {
        return "Test";
    }

    private IEnumerator SpawnEffectVFX(Being being, Effect effect)
    {
        ParticleSystem vfx = Instantiate(effect.vfx, being.transform.position, Quaternion.identity);
        vfx.Play();
        yield return new WaitForSeconds(vfx.main.duration);
    }
}