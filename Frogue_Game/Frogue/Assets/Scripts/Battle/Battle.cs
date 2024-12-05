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
        Player = _p.GetBeingSnapshot<PlayerBoard>();
        Baddie = _b.GetBeingSnapshot<BaddieBoard>();

        BattleActions = GetBattleActions(Player, Baddie);
    }

    private BattleAction[] GetBattleActions(PlayerBoard player, BaddieBoard baddie)
    {
        List<BattleAction> actions = new List<BattleAction>();

        actions.AddRange(GetActions(player, baddie));
        actions.AddRange(GetActions(baddie, player));

        return actions.ToArray();
    }

    private BattleAction[] GetActions(Board attacker, Board defender)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (Being attackerCurrent in attacker)
        {
            if (!defender.IsDead)
            {
                //damage
                Being defenseNext = defender.Next;
                DamageAction damageAction = new DamageAction(attackerCurrent, defenseNext);
                actions.Add(damageAction);

                //dead action
                if (defenseNext.BeingInfo.IsDead)
                {
                    DeadAction deadAction = new DeadAction(defender, defenseNext);
                    actions.Add(deadAction);
                }
            }
            else
            {
                //battle over
                if (attacker is PlayerBoard)
                    actions.Add(new BattleOverAction((PlayerBoard)attacker, (BaddieBoard)defender));
                else
                    actions.Add(new BattleOverAction((PlayerBoard)defender, (BaddieBoard)attacker));

                return actions.ToArray();
            }
        }

        return actions.ToArray();
    }
}