using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Battle
{
    public int PlusMinus { get; private set; } = 0;
    public BattleAction[] BattleActions { get; private set; }

    public SnapshotBeing[] PlayerSnapshot { get; private set; }
    public SnapshotBeing[] BaddieSnapshot { get; private set; }

    public Battle(PlayerBoard playerBoard, BaddieBoard baddieBoard)
    {
        SnapshotBoard playerSnapshotBoard = new SnapshotBoard(playerBoard);
        SnapshotBoard baddieSnapshotBoard = new SnapshotBoard(baddieBoard);

        BattleActions = GetBattleActions(playerSnapshotBoard, baddieSnapshotBoard);

        PlayerSnapshot = playerSnapshotBoard.AliveSnapshotBeings;
        BaddieSnapshot = baddieSnapshotBoard.AliveSnapshotBeings;
    }

    private BattleAction[] GetBattleActions(SnapshotBoard playerSnapshotBoard, SnapshotBoard baddieSnapshotBoard)
    {
        List<BattleAction> actions = new List<BattleAction>();

        actions.AddRange(GetActions(playerSnapshotBoard, baddieSnapshotBoard));
        actions.Add(new BoardSwitchAction());
        actions.AddRange(GetActions(baddieSnapshotBoard, playerSnapshotBoard));

        return actions.ToArray();
    }

    private void UpdatePlusMinus(SnapshotBoard board, BattleAction action)
    {
        if (board.IsPlayerBoard)
            PlusMinus += action.PlusMinusCost();
        else
            PlusMinus -= action.PlusMinusCost();
    }

    private BattleAction[] GetActions(SnapshotBoard attacker, SnapshotBoard defender)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (SnapshotBeing attackerCurrent in attacker.AliveSnapshotBeings)
        {
            if (!defender.IsDead)
            {
                //damage
                SnapshotBeing defenseNext = defender.Next;
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