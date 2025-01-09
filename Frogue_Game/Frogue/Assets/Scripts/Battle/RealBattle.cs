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
}