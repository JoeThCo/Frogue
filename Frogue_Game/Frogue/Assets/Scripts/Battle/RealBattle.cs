using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBattle : Battle
{
    public RealBattle(Board player, Board baddie) : base(player, baddie)
    {
        PlayerActions = GetBoardActions(player, baddie);
        BaddieActions = GetBoardActions(baddie, player);
        PlusMinus = GetPlusMinus(PlayerActions, BaddieActions);
    }

    public IEnumerator DisplayBattle() 
    {
        foreach (BattleAction action in PlayerActions) 
        {
            yield return action.DisplayAction();
        }

        foreach (BattleAction action in BaddieActions)
        {
            yield return action.DisplayAction();
        }
    }
}