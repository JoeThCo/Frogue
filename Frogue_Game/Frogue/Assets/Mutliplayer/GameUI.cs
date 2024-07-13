using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerOne;
    [SerializeField] private TextMeshProUGUI playerTwo;

    internal static string localPlayerName;

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
        Debug.Log(localPlayerName);
    }
}