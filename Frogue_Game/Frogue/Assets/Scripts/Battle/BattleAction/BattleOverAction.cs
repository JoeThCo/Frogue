using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOverAction : BattleAction
{
    public BeingBoard Winner { get; private set; }

    public BattleOverAction(BeingBoard board)
    {
        this.Winner = board;

        CalculateAction();
    }

    public override IEnumerator DisplayAction()
    {
        if (Winner.IsPlayer)
            MenuController.Instance.DisplayMenu("Win");
        else
            MenuController.Instance.DisplayMenu("GameOver");

        yield return null;
    }

    public override int Cost()
    {
        return 999;
    }

    protected override void CalculateAction()
    {
        Debug.Log(this.ToString());
    }

    public override string ToString()
    {
        if (Winner.IsPlayer)
            return "Player Wins!";
        return "CPU Wins!";
    }
}