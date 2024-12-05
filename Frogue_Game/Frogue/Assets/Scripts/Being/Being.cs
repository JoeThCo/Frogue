using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being
{
    public BeingInfo BeingInfo { get; protected set; }

    public Being(Vector2Int coords)
    {
        this.BeingInfo = new BeingInfo(ResourceLoader.GetBeingInit(), coords);
    }

    public Being(SnapshotBeing snapshotBeing)
    {
        this.BeingInfo = snapshotBeing.BeingInfo;
    }

    public SnapshotBeing MakeSnapshot() 
    {
        return new SnapshotBeing(this);
    }

    public override string ToString()
    {
        return $"{BeingInfo.ID} | ({BeingInfo.Coords})| [{BeingInfo.HP} | {BeingInfo.Attack}]";
    }
}