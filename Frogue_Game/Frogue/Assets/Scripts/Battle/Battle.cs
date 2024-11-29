using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    public PlayerBoard Player { get; private set; }
    public BaddieBoard Baddie { get; private set; }
    public int PlusMinus { get; private set; } = 0;
    public BattleAction[] BattleActions { get; private set; }

    public Battle(PlayerBoard _p, BaddieBoard _b)
    {
        Player = _p.DeepCopy<PlayerBoard>();
        Baddie = _b.DeepCopy<BaddieBoard>();

        BattleActions = GetBattleActions(Player, Baddie);
    }

    private BattleAction[] GetBattleActions(PlayerBoard player, BaddieBoard baddie)
    {
        List<BattleAction> actions = new List<BattleAction>();

        actions.AddRange(GetActions(player, baddie));
        actions.AddRange(GetActions(baddie, player));

        return actions.ToArray();
    }

    private BattleAction[] GetActions(Board a, Board b)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (Being aCurrent in a)
        {
            if (!b.IsDead)
            {
                Being bNext = b.Next;

                DamageAction damageAction = new DamageAction(aCurrent, bNext);
                actions.Add(damageAction);

                if (bNext.IsDead)
                {
                    DeadAction deadAction = new DeadAction(b, bNext);
                    actions.Add(deadAction);
                }
            }
            else
            {
                if (a is PlayerBoard)
                    actions.Add(new BattleOverAction((PlayerBoard)a, (BaddieBoard)b));
                else
                    actions.Add(new BattleOverAction((PlayerBoard)b, (BaddieBoard)a));

                return actions.ToArray();
            }
        }

        return actions.ToArray();
    }
}