using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction
{
    public BattleAction(Being a, Being b) { }

    public BattleAction(Board board, Being being) { }

    public BattleAction(Board winner, Board player) { }

    protected abstract void BeingAction();

    public abstract IEnumerator BeingDisplayAction();
}