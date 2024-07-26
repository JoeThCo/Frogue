using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] BeingHolder playerGrid;
    [SerializeField] BeingHolder baddieParty;

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

        //yield return GridFight(faster, slower);
        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightHalf();
        yield return new WaitForSeconds(1);

        //yield return GridFight(slower, faster);
        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightEnd();
    }

    public void CheckBattleOver()
    {
        if (playerGrid.HasAliveBeings || baddieParty.HasAliveBeings)
        {
            Debug.Log("Winner!");
            BeingBattleBus.EmitBattleOver();
        }
    }
}