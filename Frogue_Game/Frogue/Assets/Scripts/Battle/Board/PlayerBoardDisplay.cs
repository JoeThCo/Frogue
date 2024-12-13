using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardDisplay : Board
{
    public override void BoardInit()
    {
        base.BoardInit();
        SpawnSlotDisplays(true);
        SpawnBeingDisplays(true);
    }

    protected override void ModifyBeingDisplay(BeingDisplay beingDisplay)
    {
        base.ModifyBeingDisplay(beingDisplay);
        beingDisplay.Rotate(180);
    }
}