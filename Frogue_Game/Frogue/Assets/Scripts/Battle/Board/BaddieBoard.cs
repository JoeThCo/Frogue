using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieBoard : Board
{
    public BaddieBoard() : base() { }

    public override Vector2Int GetNextOpenSlot()
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                if (board[x, y] == null || board[x, y].BeingInfo.IsDead)
                    return new Vector2Int(x, y);
            }
        }

        return NO_SLOTS;
    }
}
