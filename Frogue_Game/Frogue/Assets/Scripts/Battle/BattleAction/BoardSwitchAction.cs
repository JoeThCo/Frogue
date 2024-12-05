using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSwitchAction : BattleAction
{
    public BoardSwitchAction() { }

    public override IEnumerator DisplayAction()
    {
        yield return null;
    }

    protected override void CalculateAction()
    {

    }

    public override string ToString()
    {
        return $"Halftime!";
    }

    public override int PlusMinusCost()
    {
        return 0;
    }
}
