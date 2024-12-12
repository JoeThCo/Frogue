using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : BattleAction
{
    public Board Board { get; private set; }
    public Being DeadSnapshotBeing { get; private set; }

    public DeadAction(Board board, Being being) : base(board, being)
    {
        this.Board = board;
        this.DeadSnapshotBeing = new Being(being);

        CalculateAction();
    }

    protected override void CalculateAction()
    {
        //handleded in GetActions()
    }

    public override IEnumerator DisplayAction()
    {
        DeadSnapshotBeing.BeingDisplay.OnDead();
        yield return new WaitForSeconds(.10f);
    }

    public override string ToString()
    {
        return $"{DeadSnapshotBeing.ID} died!";
    }

    public override int PlusMinusCost()
    {
        return DeadSnapshotBeing.Health.HP * 2;
    }
}