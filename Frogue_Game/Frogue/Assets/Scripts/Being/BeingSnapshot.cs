using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingSnapshot : Being
{
    public BeingSnapshot(Being being) : base(being)
    {
        this.BeingInfo = new BeingInfo(
            being.BeingInfo.ID,
            being.BeingInfo.HP,
            being.BeingInfo.Damage,
            being.BeingInfo.Coords);

        this.BeingDisplay = BeingDisplay;
    }
}