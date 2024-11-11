using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] BoardDisplay playerBoardDisplay;
    [SerializeField] BoardDisplay baddieBoardDisplay;

    public Board Player { get; private set; }
    public Board Baddie { get; private set; }

    private void Start()
    {
        Player = new Board();
        Baddie = new Board();

        playerBoardDisplay.BoardDisplayInit(Player);
        baddieBoardDisplay.BoardDisplayInit(Baddie);
    }
}