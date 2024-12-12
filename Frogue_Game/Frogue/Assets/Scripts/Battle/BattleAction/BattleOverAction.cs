using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOverAction : BattleAction
{
    public Board Board { get; private set; }

    public BattleOverAction(Board board) : base(board)
    {
        this.Board = board;

        CalculateAction();
    }

    public override IEnumerator DisplayAction()
    {
        if (Board.IsPlayerBoard)
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
        if (Board.IsPlayerBoard)
            return $"Player wins!";
        return $"CPU wins!";
    }

    public override int PlusMinusCost()
    {
        return 999;
    }
}