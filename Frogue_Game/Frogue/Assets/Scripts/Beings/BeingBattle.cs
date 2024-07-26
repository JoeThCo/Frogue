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

        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightHalf();
        yield return new WaitForSeconds(1);

        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightEnd();
    }

    IEnumerator GridFight(BeingHolder attacker, BeingHolder defender)
    {
        foreach (BeingSlot beingSlot in attacker.AliveBeings)
        {
            CheckBattleOver();
            if (!isBattling)
            {
                yield break;
            }
        }
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