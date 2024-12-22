using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : BattleAction
{
    public Being DeadBeing { get; private set; }

    public DeadAction(Being being)
    {
        this.DeadBeing = being;
    }

    public override IEnumerator DisplayAction()
    {
        DeadBeing.BeingDisplay.OnDead();
        yield return new WaitForSeconds(.25f);
    }

    public override int PlusMinusCost()
    {
        throw new System.NotImplementedException();
    }

    protected override void CalculateAction()
    {
        //handled?
    }

    public override string ToString()
    {
        return $"{DeadBeing.ID} died!";
    }
}