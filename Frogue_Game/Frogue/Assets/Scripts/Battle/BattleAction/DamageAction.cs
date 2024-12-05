using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public Being Attacker { get; private set; }
    public Being Defender { get; private set; }

    public DamageAction(Being attacker, Being defender) : base(attacker, defender)
    {
        this.Attacker = attacker;
        this.Defender = defender;

        BeingAction();
    }

    protected override void BeingAction()
    {
        Defender.BeingInfo.TakeDamage(Attacker.BeingInfo.Damage);
    }

    public override IEnumerator BeingDisplayAction()
    {
        Attacker.BeingDisplay.SaveReturnPostion();

        yield return Attacker.BeingDisplay.MoveTo(Defender.BeingDisplay);

        yield return Defender.BeingDisplay.OnDamage();

        yield return Attacker.BeingDisplay.MoveToReturn();
    }
}