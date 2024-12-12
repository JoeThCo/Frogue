using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public Being Attacker { get; private set; }

    public Being CalculateDefender { get; private set; }
    public Being DisplayDefender { get; private set; }

    public bool IsDead
    {
        get
        {
            return CalculateDefender.Health.IsDead;
        }
    }

    public int FinalHealth
    {
        get
        {
            return DisplayDefender.Health.HP - Attacker.Attack;
        }
    }

    public DamageAction(Being attacker, Being defender) : base(attacker, defender)
    {
        this.Attacker = attacker;
        this.CalculateDefender = defender;

        this.DisplayDefender = new Being(defender);

        CalculateAction();
    }

    public override IEnumerator DisplayAction()
    {
        Attacker.BeingDisplay.SaveReturnPostion();

        yield return Attacker.BeingDisplay.MoveTo(DisplayDefender.BeingDisplay);
        yield return DisplayDefender.BeingDisplay.OnDamage(this);
        yield return Attacker.BeingDisplay.MoveToReturn();
    }

    protected override void CalculateAction()
    {
        CalculateDefender.Health.TakeDamage(Attacker.Attack);
    }

    public override string ToString()
    {
        return $"{Attacker.ID} (HP {DisplayDefender.Health.HP}) attacked by {Attacker.ID} (ATK {Attacker.Attack}) | {DisplayDefender.Health.HP} => {FinalHealth}";
    }

    public override int PlusMinusCost()
    {
        return FinalHealth;
    }
}