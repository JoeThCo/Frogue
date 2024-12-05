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

        BeingAction();
    }

    protected override void BeingAction()
    {

    }

    public override IEnumerator BeingDisplayAction()
    {
        yield return new WaitForSeconds(.10f);
    }
}