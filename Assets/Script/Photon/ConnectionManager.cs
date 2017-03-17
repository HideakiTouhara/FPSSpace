using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : Photon.PunBehaviour  
{
	public static ConnectionManager instance;

	[SerializeField]
	private RoomSelectUI roomSelectUI;

	[SerializeField]
	private RoomInfoPanel roomInfoPanel;

	[SerializeField] private Const.ConnectionState state;
	public Const.ConnectionState ConnectionState
	{
		get{return state;}
	}
	private RoomInfo[] rooms;
	private PhotonView myPhotonView;

	public void Start () 
	{
		instance = this;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	public override void OnConnectedToMaster()
	{
		Debug.Log("ConnectedToMaster");
		state = Const.ConnectionState.Master;
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		state = Const.ConnectionState.Lobby;
		Debug.Log("JoinedLobby");
	}

	public override void OnReceivedRoomListUpdate()
	{
		rooms = PhotonNetwork.GetRoomList();
		foreach(var r in rooms) {
			string log =
				"RoomName" + r.name
				+ "userName" + r.customProperties["userName"]
				+ "userId" + r.customProperties["userId"];
			Debug.Log(log);
		}
		if(rooms.Length == 0) Debug.Log("no rooms");
		else roomSelectUI.UpdateUIList(rooms);
	}

	public void CreateRoom(string roomName)
	{
		var roomOptions = new RoomOptions();
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
		roomOptions.maxPlayers = 4;
		var hash = new ExitGames.Client.Photon.Hashtable();
		hash.Add("StandbyedCount", 0);
		roomOptions.CustomRoomProperties = hash;
		var result = PhotonNetwork.CreateRoom(roomName, roomOptions, null);
	}

	public void JoinRoom(string roomName)
	{
		var result = PhotonNetwork.JoinRoom(roomName);
		print("join room : " + result);
	}

	public override void OnJoinedRoom()
	{
		state = Const.ConnectionState.Room;
		Debug.Log(PhotonNetwork.room.name + "に入室しました。");
		roomInfoPanel.gameObject.SetActive(true);
	}
}
