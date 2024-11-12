using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] BoardDisplay playerBoardDisplay;
    [SerializeField] BoardDisplay baddieBoardDisplay;
    [Space(10)]
    [SerializeField] private PlayerModify playerModify;

    public Board Player { get; private set; }
    public Board Baddie { get; private set; }

    private void Start()
    {
        Player = new Board();
        Baddie = new Board();

        playerBoardDisplay.BoardDisplayInit(Player);
        baddieBoardDisplay.BoardDisplayInit(Baddie);

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