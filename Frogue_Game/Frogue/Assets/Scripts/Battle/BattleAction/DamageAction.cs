using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public SnapshotBeing Attacker { get; private set; }

    public SnapshotBeing CalculateDefender { get; private set; }
    public SnapshotBeing DisplayDefender { get; private set; }

    public bool IsDead
    {
        get
        {
            return CalculateDefender.BeingInfo.IsDead;
        }
    }

    public int FinalHealth
    {
        get
        {
            return DisplayDefender.BeingInfo.HP - Attacker.BeingInfo.Attack;
        }
    }

    public DamageAction(SnapshotBeing attacker, SnapshotBeing defender) : base(attacker, defender)
    {
        this.Attacker = attacker;
        this.CalculateDefender = defender;

        this.DisplayDefender = new SnapshotBeing(defender);

        CalculateAction();
    }

    public override IEnumerator DisplayAction()
    {
        Attacker.BeingInfo.BeingDisplay.SaveReturnPostion();

        yield return Attacker.BeingInfo.BeingDisplay.MoveTo(DisplayDefender.BeingInfo.BeingDisplay);
        yield return DisplayDefender.BeingInfo.BeingDisplay.OnDamage(this);
        yield return Attacker.BeingInfo.BeingDisplay.MoveToReturn();
    }

    protected override void CalculateAction()
    {
        CalculateDefender.BeingInfo.TakeDamage(Attacker.BeingInfo.Attack);
    }

    public override string ToString()
    {
        return $"{Attacker.BeingInfo.ID} (HP {DisplayDefender.BeingInfo.HP}) attacked by {Attacker.BeingInfo.ID} (ATK {Attacker.BeingInfo.Attack}) | {DisplayDefender.BeingInfo.HP} => {FinalHealth}";
    }
}