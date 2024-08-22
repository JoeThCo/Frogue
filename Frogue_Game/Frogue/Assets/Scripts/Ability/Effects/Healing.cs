using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "ScriptableObject/Effect/Healing")]
public class Healing : Effect
{
    [Range(1, 10)] public int HealingAmount;

    public override void OnEffectAdded(Being being)
    {
        being.Health.HealBeing(this);
    }
}