using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Being
{
    public Damage Damage { get; private set; }
    public Health Health { get; private set; }
    public Effects Effects { get; private set; }
    public Types Types { get; private set; }
    public BeingSO BeingInfo { get; private set; }

    public Being(BeingSO beingInfo)
    {
        this.BeingInfo = beingInfo;

        Effects = new Effects(this);
        Types = new Types(BeingInfo.GetTypes());

        Health = new Health(this, BeingInfo.GetHealth());

        Damage = new Damage(Effects, BeingInfo.GetDamage());
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        Being otherBeing = other as Being;

        return otherBeing.Damage == Damage &&
            otherBeing.Health == Health &&
            otherBeing.Types == Types &&
            otherBeing.BeingInfo == BeingInfo &&
            otherBeing.Effects == Effects;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}