using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Battle
{
    public int PlusMinus { get; private set; } = 0;
    public readonly BattleAction[] PlayerActions;
    public readonly BattleAction[] BaddieActions;

    public Battle(DisplayBoard playerBoard, DisplayBoard baddieBoard)
    {
        BeingBoard player = playerBoard.GetBeingBoard(true);
        BeingBoard baddie = baddieBoard.GetBeingBoard(false);

        PlayerActions = GetBoardAction(player, baddie);
        BaddieActions = GetBoardAction(baddie, player);
        PlusMinus = GetPlusMinus(PlayerActions, BaddieActions);

        baddieBoard.UpdateDisplay(baddie);
        playerBoard.UpdateDisplay(player);
    }

    private int GetPlusMinus(BattleAction[] player, BattleAction[] baddie)
    {
        int plusMinus = 0;
        foreach (BattleAction action in PlayerActions)
            plusMinus += action.Cost();

        foreach (BattleAction action in BaddieActions)
            plusMinus -= action.Cost();

        return plusMinus;
    }

    private BattleAction[] GetBoardAction(BeingBoard player, BeingBoard baddie)
    {
        List<BattleAction> actions = new List<BattleAction>();
        actions.AddRange(GetAttackActions(player, baddie));
        return actions.ToArray();
    }

    private BattleAction[] GetAttackActions(BeingBoard attack, BeingBoard defense)
    {
        List<BattleAction> actions = new List<BattleAction>();

        int AddAction(BeingBoard board, BattleAction action)
        {
            actions.Add(action);
            return board.IsPlayer ? action.Cost() : -action.Cost();
        }

        foreach (Being nextAttack in attack.AliveBeings)
        {
            if (!defense.IsDead)
            {
                Being nextDefense = defense.Next;
                actions.Add(new DamageAction(nextAttack, nextDefense));

                if (nextDefense.Health.IsDead) AddAction(attack, new DeadAction(nextDefense));
            }
            else
            {
                actions.Add(new BattleOverAction(attack));
                return actions.ToArray();
            }
        }

        if (defense.IsDead) actions.Add(new BattleOverAction(attack));
        return actions.ToArray();
    }
}