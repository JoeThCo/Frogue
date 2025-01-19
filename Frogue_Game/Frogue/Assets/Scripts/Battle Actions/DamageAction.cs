using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public readonly Being Attacker;
    public readonly Board AttackerBoard;

    public readonly Board DefendingBoard;
    public readonly Being PreDefense;
    public readonly Being PostDefense;

    public DamageAction(Being attacker, Being defender) : base(attacker, defender)
    {
        this.Attacker = attacker;
        this.PreDefense = defender.DeepClone();
        this.PostDefense = defender;

        CalculateAction();
    }

    public override int Cost()
    {
        return Attacker.Attack;
    }

    public override IEnumerator DisplayAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void CalculateAction()
    {
        PostDefense.GetDamaged(Attacker);
    }

    public override string ToString()
    {
        return $"{PreDefense.ID} takes {Attacker.Attack} [{PreDefense.Health} => {PostDefense.Health}]";
    }
}