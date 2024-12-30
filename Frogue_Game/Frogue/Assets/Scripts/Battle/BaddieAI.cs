using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieAI
{
    private DisplayBoard inputBoard;

    public BaddieAI(DisplayBoard displayBoard)
    {
        inputBoard = displayBoard;
    }

    public BeingBoard GetBestRandomBeingBoard(BeingBoard playerboard, int iterations)
    {
        int bestPlusMinus = 0;
        int bestIndex = 0;
        BeingBoard bestBoard = null;

        for (int i = 0; i < iterations; i++)
        {
            BeingBoard randomBoard = inputBoard.GetRandomBeingBoard();
            Battle battle = new Battle(playerboard, randomBoard);
            Debug.LogWarning($"{i}'s +-: {battle.PlusMinus}");

            if (battle.PlusMinus > bestPlusMinus)
            {
                bestPlusMinus = battle.PlusMinus;
                bestBoard = randomBoard;
                bestIndex = i;
            }
        }

        Debug.LogWarning($"Best: {bestIndex} with {bestPlusMinus}");
        return bestBoard;
    }
}