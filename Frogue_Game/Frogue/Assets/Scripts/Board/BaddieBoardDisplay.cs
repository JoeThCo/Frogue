using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieBoardDisplay : BoardDisplay
{
    public override void BoardDisplayInit(Board board)
    {
        base.BoardDisplayInit(board);
        SpawnBeingDisplays(board, false);
        SpawnSlotDisplays(false);
    }

    protected override void ModifyBeingDisplay(BeingDisplay beingDisplay)
    {
        base.ModifyBeingDisplay(beingDisplay);
    }
}