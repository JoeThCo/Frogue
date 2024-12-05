using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : BattleAction
{
    public SnapshotBoard Board { get; private set; }
    public SnapshotBeing DeadSnapshotBeing { get; private set; }

    public DeadAction(SnapshotBoard board, SnapshotBeing being) : base(board, being)
    {
        this.Board = board;
        this.DeadSnapshotBeing = new SnapshotBeing(being);

        CalculateAction();
    }

    protected override void CalculateAction()
    {
        //handleded in GetActions()
    }

    public override IEnumerator DisplayAction()
    {
        DeadSnapshotBeing.BeingInfo.BeingDisplay.OnDead();
        yield return new WaitForSeconds(.10f);
    }

    public override string ToString()
    {
        return $"{DeadSnapshotBeing.BeingInfo.ID} died!";
    }
}