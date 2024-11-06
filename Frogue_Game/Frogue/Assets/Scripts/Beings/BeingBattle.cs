using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TreeEditor;

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
        isBattling = true;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
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
        BattleAction[] playerAbilities = Ability(playerBeingHolder);
        BattleAction[] playerOffense = Offense(playerBeingHolder, baddieBeingHolder);

        BattleAction[] baddieAbilities = Ability(playerBeingHolder);
        BattleAction[] baddieOffense = Offense(baddieBeingHolder, playerBeingHolder);

        Debug.Log($"Player {playerOffense.Length}");
        Debug.Log($"Baddie {baddieOffense.Length}");
    }

    private BattleAction[] Ability(BeingHolder holder)
    {
        List<BattleAction> output = new List<BattleAction>();

        foreach (Being being in holder.GetAliveBeings())
        {
            Ability ability = being.BeingInfo.GetAbility();
            //if (ability != null)
            // yield return ability.AbilityCheck(beingHolder);
        }

        return output.ToArray();
    }

    private BattleAction[] Offense(BeingHolder frogs, BeingHolder baddie)
    {
        List<BattleAction> output = new List<BattleAction>();

        foreach (Being being in frogs.GetAliveBeings())
        {
            Being next = baddie.GetNext();

            DamageAction damageAction = new DamageAction(being, next);
            damageAction.Calculate();
            output.Add(damageAction);
        }

        return output.ToArray();
    }
}