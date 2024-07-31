using UnityEngine;
using DG.Tweening;

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

    public Being[] GetBulkAttackedBeings(BulkAttack bulkAttack) 
    {
        return bulkAttack.GetBeingsToAttack(allSlots);
    }
}