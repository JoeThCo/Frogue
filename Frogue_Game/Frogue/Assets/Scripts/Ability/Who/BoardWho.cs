using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardWho", menuName = "ScriptableObject/Who/BoardWho")]
public class BoardWho : Who
{
    [SerializeField] private List<int> rows = new List<int>();
    [SerializeField] private List<int> columns = new List<int>();

    public override bool IsInWho(BeingSlot slot)
    {
        //Debug.Log($"{base.IsInWho(slot)} {IsInRows(slot)} {IsInColumns(slot)}");
        return IsInRows(slot) || IsInColumns(slot);
    }

    private bool IsInRows(BeingSlot slot)
    {
        return IsBeingInSlot(slot) && rows.Contains(slot.Coords.y);
    }

    private bool IsInColumns(BeingSlot slot)
    {
        return IsBeingInSlot(slot) && columns.Contains(slot.Coords.x);
    }
}