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

        BeingAction();
    }

    public override IEnumerator BeingDisplayAction()
    {
        if (isPlayerWinner)
        {
            Debug.Log("You Win!");
            ResourceLoader.SpawnSoundEffect("PlayerWin");
        }
        else
        {
            Debug.Log("Game Over!");
            ResourceLoader.SpawnSoundEffect("GameOver");
        }
        yield return this;
    }

    protected override void BeingAction()
    {
        isPlayerWinner = !Player.IsDead && Baddie.IsDead;
    }
}