using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction
{
    public BattleAction() { }
    public BattleAction(SnapshotBoard snapshotBoard) { }

    public BattleAction(SnapshotBeing attacker, SnapshotBeing defender) { }

    public BattleAction(SnapshotBoard snapshotBoard, SnapshotBeing being) { }

    protected abstract void CalculateAction();

    public abstract IEnumerator DisplayAction();

    public abstract int PlusMinusCost();
}