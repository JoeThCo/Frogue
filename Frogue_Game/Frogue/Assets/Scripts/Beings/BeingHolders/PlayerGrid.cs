using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGrid : BeingHolder
{
    [SerializeField] private float cellSize = 2;

    public override Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.Coords * cellSize) - (Vector2.one * gridSize) + ((Vector2.up + Vector2.right) * gridSize * 0.5f);
        if (beingSlot.Coords.y % 2 == 0)
            return position + (Vector2.left * cellSize * 0.5f);
        return position;
    }

    public void SwapBeingControllers(BeingSlot selected, BeingSlot other)
    {
        if (other.Being == null)
        {
            selected.Being.transform.SetParent(other.transform);

            other.Being = selected.Being;
            selected.Being = null;

            other.Being.transform.localPosition = Vector2.zero;
        }
        else
        {
            BeingSlot tempSlot = selected;
            selected.Being.transform.SetParent(other.transform);
            other.Being.transform.SetParent(tempSlot.transform);

            Being tempController = selected.Being;
            selected.Being = other.Being;
            other.Being = tempController;

            selected.Being.transform.localPosition = Vector3.zero;
            other.Being.transform.localPosition = Vector3.zero;
        }
    }
}