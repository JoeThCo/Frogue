using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] BeingGrid playerGrid;
    [SerializeField] BeingGrid baddieGrid;

    public static bool isBattling = true;

    private void Start()
    {
        BeingBattleBus.BattleOver += BeingBattleBus_BattleOver;
    }

    private void BeingBattleBus_BattleOver()
    {
        isBattling = false;
    }

    public void Fight()
    {
        StartCoroutine(FightI());
    }

    public IEnumerator FightI()
    {
        BeingBattleBus.EmitFightStart();

        yield return GridFight(playerGrid, baddieGrid);

        BeingBattleBus.EmitFightHalf();
        yield return new WaitForSeconds(1);

        yield return GridFight(baddieGrid, playerGrid);

        BeingBattleBus.EmitFightEnd();
    }

    IEnumerator GridFight(BeingGrid offense, BeingGrid defense)
    {
        foreach (Being current in offense.GetAliveBeings())
        {
            BattleState battleState = new BattleState(current, defense.GetFirstBeing(), offense, defense);
            yield return current.DamageTween(defense.GetFirstBeing());

            CheckBattleOver();
            if (!isBattling)
            {
                yield break;
            }
        }
    }

    public void CheckBattleOver()
    {
        if (playerGrid.HasAliveBeings() || baddieGrid.HasAliveBeings())
        {
            Debug.Log("Winner!");
            BeingBattleBus.EmitBattleOver();
        }
    }
}