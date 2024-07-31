using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkAttack : ScriptableObject
{
    public virtual Being[] GetBeingsToAttack(BeingSlot[,] allSlots)
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in allSlots)
        {
            if (IsInBulkAttack(slot))
                output.Add(slot.Being);
        }

        return output.ToArray();
    }

    protected virtual bool IsInBulkAttack(BeingSlot slot)
    {
        return slot.Being;
    }
}