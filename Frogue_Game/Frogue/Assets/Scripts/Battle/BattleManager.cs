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

    public static System.Random Random;

    private void Start()
    {
        Random = new System.Random(Seed);
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
        Debug.Log("Fight");
        Battle battle = new Battle(Player, Baddie);
        Debug.Log($"Battle Actions: {battle.BattleActions.Length}");

        yield return StartCoroutine(DisplayBattle(battle));

        ApplyBattle(battle);
    }

    private IEnumerator DisplayBattle(Battle battle)
    {
        foreach (BattleAction action in battle.BattleActions)
            yield return action.Display();
    }

    private void ApplyBattle(Battle battle)
    {
        Player.UpdateBoard(battle.Player);
        Baddie.UpdateBoard(battle.Baddie);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Player.PrintBoard();

        if (Input.GetKeyDown(KeyCode.F))
            Fight();
    }
}