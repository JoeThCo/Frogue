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
        ParticleSystem dead = ResourceLoader.SpawnParticle("Dead", DeadBeing);
        yield return new WaitForSeconds(dead.startDelay);
        DeadBeing.BeingDisplay.OnDead();
    }

    public override int Cost()
    {
        return DeadBeing.Health.MaxHP;
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