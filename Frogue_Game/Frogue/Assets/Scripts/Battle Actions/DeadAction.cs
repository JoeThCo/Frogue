using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : BattleAction
{
    public readonly Being DeadBeing;

    public DeadAction(Being deadBeing) : base(deadBeing)
    {
        this.DeadBeing = deadBeing;

        CalculateAction();
    }

    public override int Cost()
    {
        return DeadBeing.Attack;
    }

    public override IEnumerator DisplayAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void CalculateAction()
    {

    }

    public override string ToString()
    {
        return $"{DeadBeing.ID} died!";
    }
}