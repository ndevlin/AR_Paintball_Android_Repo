using UnityEngine;

public class MobileNetwork_Cube : Photon.PunBehaviour
{
    private string roomName;

    private void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		roomName = "testing";
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void OnGUI()
    {
        GUILayout.Label("            " + PhotonNetwork.connectionStateDetailed.ToString());
    }

	public override void OnJoinedLobby()
	{
		PhotonNetwork.JoinRoom(roomName);
		base.OnJoinedLobby ();
	}

    public override void OnJoinedRoom()
    {
        //Use PhotonNetwork.Instantiate to create a "PhoneCube" across the network
        Debug.Log("Trying to add PhoneCube");

        GameObject cube = PhotonNetwork.Instantiate("PhoneCube", new Vector3(0, 0, 0), Quaternion.identity, 0);

        GetComponent<GyroController>().ControlledObject = cube;

        base.OnJoinedRoom ();
	}
}