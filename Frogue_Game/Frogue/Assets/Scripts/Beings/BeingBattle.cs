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

    public static event Action PlayerWin;
    public static event Action GameOver;

    private void Start()
    {
        BattleOver += BeingBattleBus_BattleOver;
        PlayerWin += BeingBattle_PlayerWin;
        GameOver += BeingBattle_GameOver;

        isBattling = true;
    }

    private void OnDisable()
    {
        BattleOver -= BeingBattleBus_BattleOver;
        PlayerWin -= BeingBattle_PlayerWin;
        GameOver -= BeingBattle_GameOver;
    }

    private void BeingBattle_GameOver()
    {
        MenuController.Instance.ShowMenu("GameOver");
    }

    private void BeingBattle_PlayerWin()
    {
        MenuController.Instance.ShowMenu("PlayerWin");
    }

    private void BeingBattleBus_BattleOver()
    {
        isBattling = false;
    }

    public void Fight()
    {
        StartCoroutine(FightI());
    }

    private IEnumerator FightI()
    {
        FightStart?.Invoke();

        yield return AbilityCheck(playerGrid);
        yield return FrogAttack(playerGrid, baddieParty);

        if (!isBattling)
        {
            yield break;
        }

        FightHalf?.Invoke();
        yield return new WaitForSeconds(.5f);

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

        foreach (Being being in frogs.GetBeingsToAttack(baddieSo.GetRandomBulkAttack()))
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
                if (ability != null)
                    yield return ability.AbilityCheck(beingHolder);
            }
        }
    }

    private void CheckBattleOver()
    {
        if (playerGrid.IsDead())
        {
            BattleOver?.Invoke();
            GameOver?.Invoke();
        }

        if (baddieParty.IsDead())
        {
            BattleOver?.Invoke();
            PlayerWin?.Invoke();
        }
    }
}