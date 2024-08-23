using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeWho", menuName = "ScriptableObject/Who/TypeWho")]
public class TypeWho : Who, IDescription
{
    [SerializeField] private List<BeingType> beingTypes = new List<BeingType>();

    public string GetDescription()
    {
        string output = "Is Type ";

        for (int i = 0; i < beingTypes.Count; i++)
        {
            BeingType current = beingTypes[i];
            output += current.name;

            if (beingTypes.Count > 1) continue;

            if (i > 0)
                output += ", ";
            else if (i == beingTypes.Count - 1)
                output += "or ";
        }

        return output;
    }

    public override bool IsInWho(BeingSlot slot)
    {
        return IsBeingInSlot(slot) && IsBeingOfTypes(slot);
    }

    private bool IsBeingOfTypes(BeingSlot slot)
    {
        return slot.Being.Types.IsBeingOfTypes(beingTypes);
    }
}
