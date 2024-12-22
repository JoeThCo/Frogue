using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public Being Attacker { get; private set; }
    public Being PostDefense { get; private set; }
    public Being PreDefense { get; private set; }

    public int HealthOutput { get { return PreDefense.Health.HP - Attacker.Attack; } }

    public DamageAction(Being attacker, Being defense) : base(attacker, defense)
    {
        this.Attacker = new Being(attacker);

        this.PostDefense = defense;
        this.PreDefense = new Being(PostDefense);

        CalculateAction();
    }

    public override IEnumerator DisplayAction()
    {
        Attacker.BeingDisplay.SaveReturnPostion();

        yield return Attacker.BeingDisplay.MoveTo(PostDefense.BeingDisplay);

        PostDefense.BeingDisplay.OnDamage(this);
        ResourceLoader.SpawnParticle("Damage", PostDefense.BeingDisplay);

        yield return Attacker.BeingDisplay.MoveToReturn();
    }

    public override int PlusMinusCost()
    {
        throw new System.NotImplementedException();
    }

    protected override void CalculateAction()
    {
        PostDefense.Health.TakeDamage(Attacker.Attack);
    }

    public override string ToString()
    {
        return $"{PostDefense.ID} -{Attacker.Attack} HP | {PreDefense.Health.HP} => {HealthOutput}";
    }
}