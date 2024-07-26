using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieParty : BeingHolder
{
    public override void Start()
    {
        base.Start();
    }

    public override Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        return new Vector3(beingSlot.Coords.x, beingSlot.Coords.y, 0);
    }
}