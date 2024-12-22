using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Battle
{
    public int PlusMinus { get; private set; } = 0;
    public BattleAction[] BattleActions { get; private set; }

    public Battle(DisplayBoard playerBoard, DisplayBoard baddieBoard)
    {
        BeingBoard player = playerBoard.GetBeingBoard(true);
        BeingBoard baddie = baddieBoard.GetBeingBoard(false);

        List<BattleAction> actions = new List<BattleAction>();

        actions.AddRange(GetAttackActions(player, baddie));
        actions.AddRange(GetAttackActions(baddie, player));

        baddieBoard.UpdateDisplay(baddie);
        playerBoard.UpdateDisplay(player);

        BattleActions = actions.ToArray();
    }

    private BattleAction[] GetAttackActions(BeingBoard attack, BeingBoard defense)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (Being nextAttack in attack.Board)
        {
            if (nextAttack == null) continue;
            if (nextAttack.Health.IsDead) continue;

            if (!defense.IsDead)
            {
                Being nextDefense = defense.Next;
                actions.Add(new DamageAction(nextAttack, nextDefense));

                if (nextDefense.Health.IsDead)
                {
                    actions.Add(new DeadAction(nextDefense));
                }
            }
            else
            {
                actions.Add(new BattleOverAction(attack));
                return actions.ToArray();
            }
        }

        if (defense.IsDead)
        {
            actions.Add(new BattleOverAction(attack));
        }

        return actions.ToArray();
    }
}