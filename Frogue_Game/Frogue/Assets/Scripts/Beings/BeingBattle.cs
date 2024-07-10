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

        BeingGrid faster;
        BeingGrid slower;

        Debug.LogFormat("Player: {0} vs Baddie: {1}", playerGrid.GridSpeed, baddieGrid.GridSpeed);
        if (playerGrid.GridSpeed >= baddieGrid.GridSpeed)
        {
            faster = playerGrid;
            slower = baddieGrid;
        }
        else
        {
            faster = baddieGrid;
            slower = playerGrid;
        }

        yield return GridFight(faster, slower);
        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightHalf();
        yield return new WaitForSeconds(1);

        yield return GridFight(slower, faster);
        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightEnd();
    }

    IEnumerator GridFight(BeingGrid offense, BeingGrid defense)
    {
        foreach (Being current in offense.AliveBeings)
        {
            BattleState battleState = new BattleState(current, defense.GetFirstBeing(), offense, defense);

            CheckBattleOver();
            if (!isBattling)
            {
                yield break;
            }
        }
    }

    public void CheckBattleOver()
    {
        if (playerGrid.HasAliveBeings || baddieGrid.HasAliveBeings)
        {
            Debug.Log("Winner!");
            BeingBattleBus.EmitBattleOver();
        }
    }
}