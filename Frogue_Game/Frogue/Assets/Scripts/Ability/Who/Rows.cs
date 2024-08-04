using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rows", menuName = "ScriptableObject/Who/Rows")]
public class Rows : Who
{
    [SerializeField] int divideBy = 0;
    [SerializeField] int result = 0;

    public override bool IsInWho(BeingSlot slot)
    {
        return base.IsInWho(slot) && slot.Coords.y % divideBy == result;
    }
}