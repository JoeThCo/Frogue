using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public string playerName;

    public override void OnStartServer()
    {
        playerName = (string)connectionToClient.authenticationData;
    }

    public override void OnStartLocalPlayer()
    {
        GameUI.localPlayerName = playerName;
        Debug.Log(playerName);
    }
}