using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability/Ability")]
public class Ability : ScriptableObject
{
    public Who WhoToTrigger;
    public Who WhoToApplyEffect;
    [Space(10)]
    public Effect[] effects;

    public IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        if (IsTriggering(beingHolder))
        {
            foreach (Being being in WhoToApplyEffect.GetWho(beingHolder))
            {
                foreach (Effect effect in effects)
                {
                    being.Effects.AddEffect(effect);
                    yield return SpawnEffectVFX(being, effect);
                }
            }
        }
    }

    private bool IsTriggering(BeingHolder beingHolder)
    {
        return WhoToTrigger.GetWho(beingHolder).Length > 0;
    }

    private IEnumerator SpawnEffectVFX(Being being, Effect effect)
    {
        ParticleSystem vfx = Instantiate(ResourceManager.GetVFX(effect), being.transform.position, Quaternion.identity);
        vfx.Play();

        yield return new WaitForSeconds(vfx.main.duration);
    }
}