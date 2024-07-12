using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI connectingText;
    [SerializeField] private byte maxPlayersPerRoom = 2;

    bool isConnecting;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
    }

    private void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    public void Join()
    {
        if (isConnecting)
        {
            connectingText.gameObject.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("PUN Basics Launcher: OnConnectedToMaster() called!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("PUN Basics Launcher: OnDisconnected() called!");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Launcher: OnJoinedRoom() called!");
        PhotonNetwork.LoadLevel(1);
    }
}