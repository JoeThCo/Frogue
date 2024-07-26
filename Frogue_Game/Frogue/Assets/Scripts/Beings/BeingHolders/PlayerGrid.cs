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
        if (other.BeingController == null)
        {
            selected.BeingController.transform.SetParent(other.transform);

            other.BeingController = selected.BeingController;
            selected.BeingController = null;

            other.BeingController.transform.localPosition = Vector2.zero;
        }
        else
        {
            BeingSlot tempSlot = selected;
            selected.BeingController.transform.SetParent(other.transform);
            other.BeingController.transform.SetParent(tempSlot.transform);

            BeingController tempController = selected.BeingController;
            selected.BeingController = other.BeingController;
            other.BeingController = tempController;

            selected.BeingController.transform.localPosition = Vector3.zero;
            other.BeingController.transform.localPosition = Vector3.zero;
        }
    }
}