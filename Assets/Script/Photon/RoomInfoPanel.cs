using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoPanel : MonoBehaviour {

	[SerializeField]
	private Text infoLabel;

	[SerializeField]
	private Button startButton;

	Room room;

	// Use this for initialization
	void Start () 
	{
		startButton.onClick.AddListener(() => {
			GameManager.instance.LoadScene(Const.Scene.testscene);
		});
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PhotonNetwork.inRoom)
		{
			if(room == null)
				room = PhotonNetwork.room;
			string info =
				"room name : " + room.name + "\n"
				+ "player count : " + room.playerCount + "/" + room.maxPlayers + "人\n";
			infoLabel.text = info;
		}
	}
}
