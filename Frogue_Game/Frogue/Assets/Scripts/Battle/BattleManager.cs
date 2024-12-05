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

    public PlayerBoard Player { get; private set; }
    public BaddieBoard Baddie { get; private set; }

    private PlayerModify playerModify;

    public static Camera MainCamera { get; private set; }
    public static System.Random Random { get; private set; }

    private void Start()
    {
        Random = new System.Random(Seed);
        MainCamera = Camera.main;

        ResourceLoader.Load();

        Player = new PlayerBoard();
        Baddie = new BaddieBoard();

        playerBoardDisplay.BoardDisplayInit(Player);
        baddieBoardDisplay.BoardDisplayInit(Baddie);

        playerModify = playerBoardDisplay.GetComponent<PlayerModify>();
        playerModify.PlayerModifyInit(Player);
    }

    public void Fight()
    {
        StartCoroutine(FightI());
    }

    private IEnumerator FightI()
    {
        Battle battle = new Battle(Player, Baddie);
        Debug.LogWarning($"Battle Actions: {battle.BattleActions.Length} for +/- ({battle.PlusMinus})");

        Player.Update(battle.PlayerSnapshot);
        Baddie.Update(battle.BaddieSnapshot);

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
        if (Input.GetKeyDown(KeyCode.Space))
            Player.PrintBoard();

        if (Input.GetKeyDown(KeyCode.F))
            Fight();
    }
}