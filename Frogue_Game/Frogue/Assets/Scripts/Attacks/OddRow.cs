using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OddRow", menuName = "ScriptableObject/BulkAttack/OddRows")]
public class OddRow : BulkAttack
{
    protected override bool IsInBulkAttack(BeingSlot slot)
    {
        return base.IsInBulkAttack(slot) && slot.Coords.x % 2 == 1;
    }
}
