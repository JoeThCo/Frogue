using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvenRow", menuName = "ScriptableObject/BulkAttack/EvenRows")]
public class EvenRow : BulkAttack
{
    protected override bool IsInBulkAttack(BeingSlot slot)
    {
        return base.IsInBulkAttack(slot) && slot.Coords.x % 2 == 0;
    }
}