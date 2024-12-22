using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction
{
    public BattleAction() { }

    public BattleAction(Being deadBeing) { }

    public BattleAction(Being attacker, Being defender) { }

    protected abstract void CalculateAction();

    public abstract IEnumerator DisplayAction();

    public abstract int PlusMinusCost();
}