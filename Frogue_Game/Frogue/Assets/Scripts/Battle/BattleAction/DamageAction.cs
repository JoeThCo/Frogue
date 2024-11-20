using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BattleAction
{
    public Being a { get; private set; }
    public Being b { get; private set; }

    public DamageAction(Being a, Being b) : base(a, b)
    {
        this.a = a;
        this.b = b;

        Calculate();
    }

    protected override void Calculate()
    {
        b.TakeDamage(a);
    }

    public override IEnumerator Display()
    {
        yield return a.BeingDisplay.MoveToandFrom(b.BeingDisplay);
    }
}