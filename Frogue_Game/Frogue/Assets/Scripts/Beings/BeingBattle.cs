using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] BeingHolder playerBeingHolder;
    [SerializeField] BeingHolder baddieBeingHolder;

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

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        isBattling = true;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        BattleOver -= BeingBattleBus_BattleOver;
        PlayerWin -= BeingBattle_PlayerWin;
        GameOver -= BeingBattle_GameOver;

        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void BeingBattle_GameOver()
    {
        SceneMenuController.Instance.ShowMenu("GameOver");
    }

    private void BeingBattle_PlayerWin()
    {
        SceneMenuController.Instance.ShowMenu("PlayerWin");
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

        yield return AbilityCheck(playerBeingHolder);
        yield return FrogAttack(playerBeingHolder, baddieBeingHolder);

        if (!isBattling)
            yield break;

        FightHalf?.Invoke();
        yield return new WaitForSeconds(.5f);

        yield return BaddieAttack(baddieBeingHolder, playerBeingHolder);
        if (!isBattling)
            yield break;

        FightEnd?.Invoke();
    }

    IEnumerator FrogAttack(BeingHolder frogs, BeingHolder baddie)
    {
        foreach (Being being in frogs.GetAliveBeings())
        {
            yield return being.DamageTween(baddie.GetNext());

            CheckBattleOver();
            if (!isBattling)
                yield break;
        }
    }

    IEnumerator BaddieAttack(BeingHolder baddie, BeingHolder frogs)
    {
        BaddieSO baddieSo = (BaddieSO)baddie.GetNext().BeingInfo;

        foreach (Being being in frogs.GetBeingsToAttack(baddieSo.GetRandomBulkAttack()))
        {
            yield return baddie.GetNext().DamageTween(being);

            CheckBattleOver();
            if (!isBattling)
                yield break;
        }
    }

    IEnumerator AbilityCheck(BeingHolder beingHolder)
    {
        foreach (Being being in beingHolder.GetAliveBeings())
        {
            Ability ability = being.BeingInfo.GetAbility();
            if (ability != null)
                yield return ability.AbilityCheck(beingHolder);
        }
    }

    private void CheckBattleOver()
    {
        if (playerBeingHolder.IsDead())
        {
            BattleOver?.Invoke();
            GameOver?.Invoke();
        }

        if (baddieBeingHolder.IsDead())
        {
            BattleOver?.Invoke();
            PlayerWin?.Invoke();
        }
    }
}