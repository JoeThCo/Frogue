using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : BattleAction
{
    public Being Being { get; private set; }
    public Board Board { get; private set; }

    public DeadAction(Board board, Being being) : base(board, being)
    {
        this.Being = being;
        this.Board = board;

        Calculate();
    }

    protected override void Calculate()
    {
        Board.Remove(Being);
    }

    public override IEnumerator Display()
    {
        Being.BeingDisplay.OnDead();
        yield return new WaitForSeconds(.10f);
    }
}