using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOverAction : BattleAction
{
    public SnapshotBoard SnapshotBoard { get; private set; }

    public BattleOverAction(SnapshotBoard snapshotBoard) : base(snapshotBoard)
    {
        this.SnapshotBoard = snapshotBoard;

        CalculateAction();
    }

    public override IEnumerator DisplayAction()
    {
        if (SnapshotBoard.IsPlayerBoard)
            ResourceLoader.SpawnSoundEffect("PlayerWin");
        else
            ResourceLoader.SpawnSoundEffect("GameOver");
        yield return this;
    }

    protected override void CalculateAction()
    {
        //handleded in GetActions()
    }

    public override string ToString()
    {
        if (SnapshotBoard.IsPlayerBoard)
            return $"Player wins!";
        return $"CPU wins!";
    }

    public override int PlusMinusCost()
    {
        return 999;
    }
}