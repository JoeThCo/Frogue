using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] BoardDisplay playerBoardDisplay;
    [SerializeField] BoardDisplay baddieBoardDisplay;

    public PlayerBoard Player { get; private set; }
    public BaddieBoard Baddie { get; private set; }

    private PlayerModify playerModify;

    private void Start()
    {
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
        Debug.Log("Fight");
        Battle battle = new Battle(Player, Baddie);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Player.PrintBoard();

        if (Input.GetKeyDown(KeyCode.F))
            Fight();
    }
}