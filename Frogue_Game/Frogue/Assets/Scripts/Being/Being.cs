using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being
{
    public BeingInfo BeingInfo { get; protected set; }
    public BeingDisplay BeingDisplay { get; protected set; }

    public Being(Being being) { }

    public Being(Vector2Int coords)
    {
        this.BeingInfo = new BeingInfo(ResourceLoader.GetBeingInit(), coords);
    }

    public BeingSnapshot MakeSnapshot() 
    {
        return new BeingSnapshot(this);
    }

    public void SetBeingDisplay(BeingDisplay beingDisplay)
    {
        this.BeingDisplay = beingDisplay;
    }

    public override string ToString()
    {
        return $"{BeingInfo.ID} | ({BeingInfo.Coords})| [{BeingInfo.HP} | {BeingInfo.Damage}]";
    }
}