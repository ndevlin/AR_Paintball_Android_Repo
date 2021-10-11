using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour
{
    private string roomName;

    private void OnGUI()
    {
        GUILayout.Label("           " + PhotonNetwork.connectionStateDetailed.ToString());
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = "testing";
        Screen.orientation = ScreenOrientation.Portrait;
    }


    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRoom(roomName);
        base.OnJoinedLobby();
    }


    public override void OnJoinedRoom()
	{
		GetComponent<MobileShooter>().Activate();
		base.OnJoinedRoom ();
    }


}
