using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Who : ScriptableObject
{
    public virtual Being[] GetWho(BeingHolder beingHolder)
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in beingHolder.GetAliveBeingSlots())
            if (IsInWho(slot))
                output.Add(slot.Being);

        return output.ToArray();
    }

    public virtual bool IsInWho(BeingSlot slot)
    {
        return false;
    }

    protected bool IsBeingInSlot(BeingSlot slot)
    {
        return slot != null && slot.Being != null;
    }
}