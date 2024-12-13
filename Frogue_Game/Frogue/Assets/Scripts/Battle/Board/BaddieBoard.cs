using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieBoard : Board
{
    public override void BoardInit()
    {
        base.BoardInit();
        SpawnSlotDisplays(false);
        SpawnBeingDisplays(false);
    }

    protected override void ModifyBeingDisplay(BeingDisplay beingDisplay)
    {
        base.ModifyBeingDisplay(beingDisplay);
    }
}