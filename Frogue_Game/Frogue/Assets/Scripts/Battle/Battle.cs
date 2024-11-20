using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    private Board Player { get; set; }
    private Board Baddie { get; set; }

    public int PlusMinus { get; private set; } = 0;
    public BattleAction[] BattleActions { get; private set; }

    public Battle(Board _p, Board _b)
    {
        Player = _p.DeepCopy();
        Baddie = _b.DeepCopy();

        BattleActions = GetBattleActions(Player, Baddie);
    }

    private BattleAction[] GetBattleActions(Board player, Board baddie)
    {
        List<BattleAction> actions = new List<BattleAction>();

        actions.AddRange(GetDamageActions(player, baddie));
        actions.AddRange(GetDamageActions(baddie, player));

        return actions.ToArray();
    }

    private BattleAction[] GetDamageActions(Board a, Board b)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (Being aCurrent in a)
        {
            Being bNext = b.GetNextBeing();

            DamageAction damageAction = new DamageAction(aCurrent, bNext);
            actions.Add(damageAction);

            if (bNext.IsDead)
            {
                DeadAction deadAction = new DeadAction(b, bNext);
                actions.Add(deadAction);
            }
        }

        return actions.ToArray();
    }
}