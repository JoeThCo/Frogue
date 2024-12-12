using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Battle
{
    public int PlusMinus { get; private set; } = 0;
    public BattleAction[] BattleActions { get; private set; }

    public Being[] PlayerOutput { get; private set; }
    public Being[] BaddieOutput { get; private set; }

    public Battle(PlayerBoard playerBoard, BaddieBoard baddieBoard)
    {
        Board playerSnapshotBoard = new Board(playerBoard);
        Board baddieSnapshotBoard = new Board(baddieBoard);

        BattleActions = GetBattleActions(playerSnapshotBoard, baddieSnapshotBoard);

        PlayerOutput = playerSnapshotBoard.AliveBeings;
        BaddieOutput = baddieSnapshotBoard.AliveBeings;
    }

    private BattleAction[] GetBattleActions(Board playerSnapshotBoard, Board baddieSnapshotBoard)
    {
        List<BattleAction> actions = new List<BattleAction>();

        actions.AddRange(GetActions(playerSnapshotBoard, baddieSnapshotBoard));
        actions.Add(new BoardSwitchAction());
        actions.AddRange(GetActions(baddieSnapshotBoard, playerSnapshotBoard));

        return actions.ToArray();
    }

    private void UpdatePlusMinus(Board board, BattleAction action)
    {
        if (board.IsPlayerBoard)
            PlusMinus += action.PlusMinusCost();
        else
            PlusMinus -= action.PlusMinusCost();
    }

    private BattleAction[] GetActions(Board attacker, Board defender)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (Being attackerCurrent in attacker.AliveBeings)
        {
            if (!defender.IsDead)
            {
                //damage
                Being defenseNext = defender.Next;
                DamageAction damageAction = new DamageAction(attackerCurrent, defenseNext);
                UpdatePlusMinus(attacker, damageAction);

                actions.Add(damageAction);

                //dead action
                if (damageAction.IsDead)
                {
                    DeadAction deadAction = new DeadAction(defender, defenseNext);
                    UpdatePlusMinus(defender, deadAction);

                    actions.Add(deadAction);
                }
            }
            else
            {
                //battle over
                BattleOverAction battleOverAction = new BattleOverAction(attacker);
                actions.Add(battleOverAction);
                UpdatePlusMinus(attacker, battleOverAction);

                return actions.ToArray();
            }
        }

        if (defender.IsDead)
            actions.Add(new BattleOverAction(attacker));

        return actions.ToArray();
    }
}