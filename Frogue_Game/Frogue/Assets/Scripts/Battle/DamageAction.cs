using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public Being Attacker { get; private set; }
    public Being Damaged { get; private set; }

    public DamageAction(Being a, Being b) : base(a, b)
    {
        this.Attacker = a;
        this.Damaged = b;
    }

    public override void Calculate()
    {
        Damaged.Health.TakeDamage(Attacker);
    }

    public override IEnumerator Display()
    {
        //yield return Attacker.DamageTween(Damaged);
        yield return null;
    }
}