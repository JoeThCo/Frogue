using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability/Ability")]
public class Ability : ScriptableObject
{
    public Who WhoToTrigger;
    [Space(10)]
    public Who WhoToApplyEffect;
    public Effect[] effects;

    public IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        if (IsTriggering(beingHolder))
        {
            foreach (Being being in WhoToApplyEffect.GetWho(beingHolder))
            {
                being.Effects.AddEffect(effects);
                Debug.Log($"{being.gameObject.name} gets {effects.Length} effects!");
                yield return new WaitForSeconds(.25f);
            }
        }
    }

    private bool IsTriggering(BeingHolder beingHolder)
    {
        return WhoToTrigger.GetWho(beingHolder).Length > 0;
    }
}