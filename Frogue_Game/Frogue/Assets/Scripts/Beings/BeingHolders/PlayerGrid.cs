using UnityEngine;
using DG.Tweening;

public class PlayerGrid : BeingHolder
{
    [SerializeField] private float cellSize = 2;

    public override Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.Coords * cellSize) - (Vector2.one * gridSize.x) + ((Vector2.up + Vector2.right) * Helper.GRID_SIZE * 0.5f);
        if (beingSlot.Coords.y % 2 == 0)
            return position + (Vector2.left * cellSize * 0.5f);
        return position;
    }

    public Being[] GetBeingsToAttack(Who who)
    {
        return who.GetWho(this);
    }

    public void DisplaySelectionSlots(BeingSlot beingslot)
    {
        bool[,] filledIn = LocationWho.GetFilledDots(beingslot.Being.BeingInfo.GetAbility());

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (filledIn[x, y])
                    AllSlots[x, y].OnSelect();
                else
                    AllSlots[x, y].OnDeselect();
            }
        }
    }
}