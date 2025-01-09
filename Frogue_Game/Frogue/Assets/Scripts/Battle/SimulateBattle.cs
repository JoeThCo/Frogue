using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateBattle : Battle
{
    public readonly Board BaddieBoard;

    public SimulateBattle(Board player, Board baddie, int seed) : base(player, baddie)
    {
        Board playerBattle = new Board(player.Clone());
        Board baddieBattle = RandomizeBoard(baddie, seed);
        BaddieBoard = new Board(baddieBattle.Clone());

        PlayerActions = GetBoardActions(playerBattle, baddieBattle);
        BaddieActions = GetBoardActions(playerBattle, baddieBattle);

        PlusMinus = GetPlusMinus(PlayerActions, BaddieActions);
    }
}