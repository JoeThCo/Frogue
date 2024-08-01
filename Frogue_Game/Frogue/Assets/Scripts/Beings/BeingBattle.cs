using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] PlayerGrid playerGrid;
    [SerializeField] BaddieParty baddieParty;

    public static bool isBattling = true;

    public static event Action FightStart;
    public static event Action FightHalf;
    public static event Action FightEnd;

    public static event Action BattleOver;

    private void Start()
    {
        BattleOver += BeingBattleBus_BattleOver;
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
        FightStart?.Invoke();

        yield return AbilityCheck(playerGrid);
        yield return FrogAttack(playerGrid, baddieParty);

        if (!isBattling)
        {
            yield break;
        }

        FightHalf?.Invoke();
        yield return new WaitForSeconds(1);

        yield return BaddieAttack(baddieParty, playerGrid);
        if (!isBattling)
        {
            yield break;
        }

        FightEnd?.Invoke();
    }

    IEnumerator FrogAttack(PlayerGrid frogs, BaddieParty baddie)
    {
        foreach (Being being in frogs.GetAliveBeings())
        {
            yield return being.DamageTween(baddie.GetNext());

            CheckBattleOver();
            if (!isBattling)
            {
                yield break;
            }
        }
    }
    IEnumerator BaddieAttack(BaddieParty baddie, PlayerGrid frogs)
    {
        BaddieSO baddieSo = (BaddieSO)baddie.GetNext().BeingInfo;

        foreach (Being being in frogs.GetBulkAttackedBeings(baddieSo.GetRandomBulkAttack()))
        {
            yield return baddie.GetNext().DamageTween(being);

            CheckBattleOver();
            if (!isBattling)
            {
                yield break;
            }
        }
    }

    IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        foreach (Being being in beingHolder.GetAliveBeings())
        {
            foreach (Ability ability in being.BeingInfo.GetAbilities())
            {
                yield return ability.AbilityCheck(beingHolder);
            }
        }
    }

    public void CheckBattleOver()
    {
        if (playerGrid.IsDead() || baddieParty.IsDead())
        {
            Debug.Log("Winner!");
            BattleOver?.Invoke();
        }
    }
}