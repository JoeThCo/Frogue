using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
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

    private bool IsInRows(BeingSlot slot)
    {
        return rows.Contains(slot.Coords.y);
    }

    private bool IsInColumns(BeingSlot slot)
    {
        return columns.Contains(slot.Coords.x);
    }

    public bool IsFilledDot(int x, int y)
    {
        return rows.Contains(x) || columns.Contains(y);
    }

    public static bool[,] GetFilledDots(Ability ability)
    {
        bool[,] output = new bool[Helper.GRID_SIZE, Helper.GRID_SIZE];

        for (int x = 0; x < output.GetLength(0); x++)
        {
            for (int y = 0; y < output.GetLength(1); y++)
            {
                bool result = false;

                foreach (LocationWho locationWho in ability.LocationWhos)
                {
                    if (result) continue;
                    result = locationWho.IsFilledDot(x, y);
                }
                output[x, y] = result;
            }
        }

        return output;
    }

    public static bool[,] GetFilledDots(Ability[] abilities)
    {
        bool[,] output = new bool[Helper.GRID_SIZE, Helper.GRID_SIZE];

        for (int x = 0; x < output.GetLength(0); x++)
        {
            for (int y = 0; y < output.GetLength(1); y++)
            {
                bool result = false;

                foreach (Ability ability in abilities)
                {
                    foreach (LocationWho locationWho in ability.LocationWhos)
                    {
                        if (result) continue;
                        result = locationWho.IsFilledDot(x, y);
                    }
                }

                output[x, y] = result;
            }
        }

        return output;
    }
}