using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private int Seed;
    [Space]
    [SerializeField] BoardDisplay playerBoardDisplay;
    [SerializeField] BoardDisplay baddieBoardDisplay;
    private PlayerModify playerModify;

    public static Camera MainCamera { get; private set; }
    public static System.Random Random { get; private set; }

    private void Start()
    {
        Random = new System.Random(Seed);
        MainCamera = Camera.main;

        ResourceLoader.Load();

        playerBoardDisplay.BoardDisplayInit();
        baddieBoardDisplay.BoardDisplayInit();

        playerModify = playerBoardDisplay.GetComponent<PlayerModify>();
        playerModify.PlayerModifyInit();
    }

    public void Fight()
    {
        StartCoroutine(FightI());
    }

    private IEnumerator FightI()
    {
        Battle battle = new Battle(playerBoardDisplay, baddieBoardDisplay);
        Debug.LogWarning($"Battle Actions: {battle.BattleActions.Length} for +/- ({battle.PlusMinus})");

        yield return StartCoroutine(DisplayBattle(battle));
    }

    private IEnumerator DisplayBattle(Battle battle)
    {
        foreach (BattleAction action in battle.BattleActions)
        {
            Debug.Log(action.ToString());
            yield return action.DisplayAction();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Fight();
    }
}