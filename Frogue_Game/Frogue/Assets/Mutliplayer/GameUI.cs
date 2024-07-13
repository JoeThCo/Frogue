using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerOne;
    [SerializeField] private TextMeshProUGUI playerTwo;

    public static GameUI Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    public void SetText(string playerName)
    {
        if (playerOne.text.Equals("???"))
        {
            playerOne.SetText(playerName);
        }
        else if (playerTwo.text.Equals("???"))
        {
            playerTwo.SetText(playerName);
        }
    }
}