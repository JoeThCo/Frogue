using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle
{
    public BattleAction[] PlayerActions { get; protected set; }
    public BattleAction[] BaddieActions { get; protected set; }
    public int PlusMinus { get; protected set; }

    public Battle(Board player, Board baddie) { }

    protected int GetPlusMinus(BattleAction[] playerActions, BattleAction[] baddieActions)
    {
        int plusMinus = 0;
        foreach (BattleAction action in playerActions)
            plusMinus += action.Cost();

        foreach (BattleAction action in baddieActions)
            plusMinus -= action.Cost();
        return plusMinus;
    }

    protected BattleAction[] GetBoardActions(Board attackingBoard, Board defendingBoard)
    {
        List<BattleAction> actions = new List<BattleAction>();

        foreach (Being attackerNext in attackingBoard.AliveBeings)
        {
            if (!defendingBoard.IsDead)
            {
                Being defenderNext = defendingBoard.Next;

                DamageAction damageAction = new DamageAction(attackerNext, defenderNext);
                actions.Add(damageAction);

                if (defenderNext.IsDead)
                {
                    DeadAction deadAction = new DeadAction(defenderNext);
                    actions.Add(deadAction);
                }
            }
            else
            {
                BattleOverAction battleOverAction = new BattleOverAction(attackingBoard);
                actions.Add(battleOverAction);
                return actions.ToArray();
            }
        }

        if (defendingBoard.IsDead)
        {
            BattleOverAction battleOverAction = new BattleOverAction(attackingBoard);
            actions.Add(battleOverAction);
        }

        return actions.ToArray();
    }

    protected Board RandomizeBoard(Board input, int seed)
    {
        // Get all non-null beings
        var allBeings = input.Beings.Cast<Being>().Where(b => b != null).ToList();

        // Calculate a unique permutation based on the seed
        var permutation = GetPermutation(allBeings, seed);

        // Create a new 2D array to represent the randomized board
        Being[,] newArrangement = new Being[Board.BOARD_SIZE, Board.BOARD_SIZE];

        // Fill the new arrangement with the permutation
        int index = 0;
        for (int x = 0; x < Board.BOARD_SIZE; x++)
        {
            for (int y = 0; y < Board.BOARD_SIZE; y++)
            {
                if (index < permutation.Count)
                {
                    // Deep clone each Being to ensure no shared references
                    newArrangement[x, y] = permutation[index++].DeepClone();
                }
                else
                {
                    newArrangement[x, y] = null;
                }
            }
        }

        return new Board(newArrangement);
    }

    private List<T> GetPermutation<T>(List<T> list, int seed)
    {
        System.Random rng = new System.Random(seed);
        var result = list.ToList();

        // Fisher-Yates shuffle with seed
        for (int i = result.Count - 1; i > 0; i--)
        {
            int swapIndex = rng.Next(i + 1);
            (result[i], result[swapIndex]) = (result[swapIndex], result[i]);
        }

        return result;
    }
}