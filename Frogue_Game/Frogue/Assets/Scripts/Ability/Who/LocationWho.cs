using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationWho", menuName = "ScriptableObject/Who/LocationWho")]
public class LocationWho : Who
{
    [SerializeField] private List<int> rows = new List<int>();
    [SerializeField] private List<int> columns = new List<int>();

    public override bool IsInWho(BeingSlot slot)
    {
        //Debug.Log($"{base.IsInWho(slot)} {IsInRows(slot)} {IsInColumns(slot)}");
        return IsBeingInSlot(slot) && IsInRows(slot) || IsInColumns(slot);
    }

    public bool IsInRows(int r)
    {
        return rows.Contains(r);
    }

    private bool IsInRows(BeingSlot slot)
    {
        return rows.Contains(slot.Coords.y);
    }

    public bool IsInColumns(int c)
    {
        return columns.Contains(c);
    }

    private bool IsInColumns(BeingSlot slot)
    {
        return columns.Contains(slot.Coords.x);
    }
}