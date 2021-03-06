using UnityEngine;
using System.Collections;

public class PCNetwork : Photon.PunBehaviour
{
    // This is for Paint Ball networking at PC
    // if you are looking for LOOK-1.b, please refer to PCNetwork_Cube.cs 
    private string roomName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = GenerateRoomName();
        PhotonNetwork.sendRate = 15;
        PhotonNetwork.sendRateOnSerialize = 15;
    }

    private void OnGUI()
    {
        GUI.contentColor = Color.red;
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() + " Room Name: " + roomName);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
    }

    public override void OnCreatedRoom()
    {
		base.OnCreatedRoom();
    }

    private static string GenerateRoomName()
    {
		return "testing";
    }

}