using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "ScriptableObject/Effect/Healing")]
public class MaxHealthChange : Effect, IInstantEffect
{
    [Range(1, 10)] public int HealingAmount;

    public void OnInstantEffect(Being being)
    {
        being.Health.ChangeMaxHealth(this);
    }
}