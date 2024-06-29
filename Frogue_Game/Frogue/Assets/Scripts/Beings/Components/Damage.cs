using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Effect
{
    public int DamageAmount { get; private set; }

    public override void EffectInit()
    {
        base.EffectInit();

        DamageAmount = Random.Range(0, 6);
    }

    public override IEnumerator ApplyEffect()
    {
        base.ApplyEffect();
        yield return null;
    }
}