using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOverAction : BattleAction
{
    public PlayerBoard Player { get; private set; }
    public BaddieBoard Baddie { get; private set; }

    private bool isPlayerWinner;


    public BattleOverAction(PlayerBoard player, BaddieBoard baddie) : base(player, baddie)
    {
        this.Player = player;
        this.Baddie = baddie;

        Calculate();
    }

    public override IEnumerator Display()
    {
        if (isPlayerWinner)
            Debug.Log("You Win!");
        else
            Debug.Log("Game Over!");
        yield return this;
    }

    protected override void Calculate()
    {
        isPlayerWinner = !Player.IsDead && Baddie.IsDead;
    }
}