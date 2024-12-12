using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction
{
    public BattleAction() { }
    public BattleAction(Board snapshotBoard) { }

    public BattleAction(Being attacker, Being defender) { }

    public BattleAction(Board snapshotBoard, Being being) { }

    protected abstract void CalculateAction();

    public abstract IEnumerator DisplayAction();

    public abstract int PlusMinusCost();
}