using UnityEngine;
using DG.Tweening;

public class PlayerGrid : BeingHolder
{
    [SerializeField][Range(.15f, .5f)] private float swapTime = .25f;
    [SerializeField] private float cellSize = 2;

    public override Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.Coords * cellSize) - (Vector2.one * gridSize) + ((Vector2.up + Vector2.right) * gridSize * 0.5f);
        if (beingSlot.Coords.y % 2 == 0)
            return position + (Vector2.left * cellSize * 0.5f);
        return position;
    }

    public void SwapBeings(BeingSlot selected, BeingSlot other)
    {
        if (other.Being == null)
        {
            selected.Being.transform.SetParent(other.transform);

            other.Being = selected.Being;
            selected.Being = null;

            other.Being.transform.DOLocalMove(Vector2.zero, swapTime);
        }
        else
        {
            BeingSlot tempSlot = selected;
            selected.Being.transform.SetParent(other.transform);
            other.Being.transform.SetParent(tempSlot.transform);

            Being tempController = selected.Being;
            selected.Being = other.Being;
            other.Being = tempController;

            selected.Being.transform.DOLocalMove(Vector2.zero, swapTime);
            other.Being.transform.DOLocalMove(Vector2.zero, swapTime);
        }
    }
}