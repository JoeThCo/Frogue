using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction
{
    public BattleAction(Being a, Being b) { }

    public abstract void Calculate();

    public abstract IEnumerator Display();
}