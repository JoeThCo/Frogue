using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOverAction : BattleAction
{
    public readonly Board WinningBoard;

    public BattleOverAction(Board winner) : base(winner)
    {
        this.WinningBoard = winner;
        CalculateAction();
    }

    public override int Cost()
    {
        return 999;
    }

    public override IEnumerator DisplayAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void CalculateAction()
    {

    }

    public override string ToString()
    {
        if (WinningBoard.IsPlayer)
            return "Player Wins!";
        else
            return "CPU Wins!";
    }
}