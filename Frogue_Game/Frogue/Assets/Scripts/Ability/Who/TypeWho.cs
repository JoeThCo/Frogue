using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeWho", menuName = "ScriptableObject/Who/TypeWho")]
public class TypeWho : ConditionWho
{
    public List<BeingType> BeingTypes = new List<BeingType>();

    public override bool IsInWho(BeingSlot slot)
    {
        return IsBeingInSlot(slot) && IsBeingOfTypes(slot);
    }

    private bool IsBeingOfTypes(BeingSlot slot)
    {
        return slot.Being.Types.IsBeingOfTypes(BeingTypes);
    }
}
