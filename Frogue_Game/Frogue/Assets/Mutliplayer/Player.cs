using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Player : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI playerText;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    void OnNameChanged(string _old, string _new)
    {
        playerText.SetText(_new);
    }

    public override void OnStartLocalPlayer()
    {
        string newName = "Player" + UnityEngine.Random.Range(0, 1000);
        CmdSetUpPlayer(newName);
    }

    [Command]
    public void CmdSetUpPlayer(string name)
    {
        playerName = name;
    }
}