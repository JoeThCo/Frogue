using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Max Health", menuName = "ScriptableObject/Effect/Max Health")]
public class MaxHealthChange : Effect, IInstantEffect
{
    [Range(1, 10)] public int HealthChange;

    public void OnInstantEffect(Being being)
    {
        being.Health.ChangeMaxHealth(this);
    }
}
