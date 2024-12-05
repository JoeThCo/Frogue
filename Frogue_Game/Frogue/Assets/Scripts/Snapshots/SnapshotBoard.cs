using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnapshotBoard
{
    public SnapshotBeing[] SnapshotBeings { get; set; }

    #region Properties
    public bool IsDead
    {
        get { return AliveSnapshotBeings.Length <= 0; }
    }

    public SnapshotBeing[] AliveSnapshotBeings
    {
        get
        {
            return SnapshotBeings
                .Where(current => current != null && !current.BeingInfo.IsDead)
                .ToArray();
        }
    }

    public SnapshotBeing Next
    {
        get
        {
            return AliveSnapshotBeings[0];
        }
    }

    public bool IsPlayerBoard { get; private set; }
    #endregion

    public SnapshotBoard(PlayerBoard playerBoard)
    {
        SnapshotBeings = MakeBeingSnapshots(playerBoard);
        IsPlayerBoard = true;
    }

    public SnapshotBoard(BaddieBoard baddieBoard)
    {
        SnapshotBeings = MakeBeingSnapshots(baddieBoard);
        IsPlayerBoard = false;
    }

    private SnapshotBeing[] MakeBeingSnapshots(Board board)
    {
        List<SnapshotBeing> output = new List<SnapshotBeing>();

        foreach (Being being in board)
            if (being != null && !being.BeingInfo.IsDead)
                output.Add(being.MakeSnapshot());

        return output.ToArray();
    }
}
