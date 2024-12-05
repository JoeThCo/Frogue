using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotBeing
{
    public BeingInfo BeingInfo { get; private set; }

    /// <summary>
    /// Used to create Snapshot Beings for Snapshot Board
    /// </summary>
    /// <param name="being"></param>
    public SnapshotBeing(Being being)
    {
        this.BeingInfo = new BeingInfo(
            being.BeingInfo.ID,
            being.BeingInfo.HP,
            being.BeingInfo.StartHP,
            being.BeingInfo.Attack,
            being.BeingInfo.Coords,
            being.BeingInfo.BeingDisplay);
    }


    /// <summary>
    /// Used to take a Snapshot of a Being during Battle Actions
    /// </summary>
    /// <param name="snapshotBeing"></param>
    public SnapshotBeing(SnapshotBeing snapshotBeing)
    {
        this.BeingInfo = new BeingInfo(
            snapshotBeing.BeingInfo.ID,
            snapshotBeing.BeingInfo.HP,
            snapshotBeing.BeingInfo.StartHP,
            snapshotBeing.BeingInfo.Attack,
            snapshotBeing.BeingInfo.Coords,
            snapshotBeing.BeingInfo.BeingDisplay);
    }

    public override string ToString()
    {
        return $"{BeingInfo.ID} | ({BeingInfo.Coords})| [{BeingInfo.HP}/{BeingInfo.StartHP} | {BeingInfo.Attack}]";
    }
}