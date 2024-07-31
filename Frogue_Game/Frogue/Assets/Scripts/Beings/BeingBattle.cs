using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] PlayerGrid playerGrid;
    [SerializeField] BaddieParty baddieParty;

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

        yield return FrogAttack(playerGrid, baddieParty);

        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightHalf();
        yield return new WaitForSeconds(1);

        yield return BaddieAttack(baddieParty, playerGrid);
        if (!isBattling)
        {
            yield break;
        }

        BeingBattleBus.EmitFightEnd();
    }

    IEnumerator FrogAttack(PlayerGrid frogs, BaddieParty baddie)
    {
        foreach (Being being in frogs.GetAliveBeings())
        {
            yield return being.DamageTween(baddie.GetNext());
        }

        CheckBattleOver();
        if (!isBattling)
        {
            yield break;
        }
    }

    IEnumerator BaddieAttack(BaddieParty baddie, PlayerGrid frogs)
    {
        BaddieSO baddieSo = (BaddieSO)baddie.GetNext().BeingInfo;

        foreach (Being being in frogs.GetBulkAttackedBeings(baddieSo.GetRandomBulkAttack()))
        {
            yield return baddie.GetNext().DamageTween(being);
        }

        CheckBattleOver();
        if (!isBattling)
        {
            yield break;
        }
    }

    public void CheckBattleOver()
    {
        if (playerGrid.IsDead() || baddieParty.IsDead())
        {
            Debug.Log("Winner!");
            BeingBattleBus.EmitBattleOver();
        }
    }
}