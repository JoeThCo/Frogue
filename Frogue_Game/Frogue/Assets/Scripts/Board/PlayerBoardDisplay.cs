using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardDisplay : BoardDisplay
{
    public override void BoardDisplayInit(Board board)
    {
        base.BoardDisplayInit(board);
    }

    protected override void ModifyBeingDisplay(BeingDisplay beingDisplay)
    {
        base.ModifyBeingDisplay(beingDisplay);
        beingDisplay.Rotate(180);
    }
}