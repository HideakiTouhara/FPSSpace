using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectCell : MonoBehaviour {

	[SerializeField]
	private Text
		nameLabel,
		playerCountLabel;

	[SerializeField]
	private Button
		joinRoomButton;

	private RoomInfo roomInfo;

	public void SetValue(RoomInfo room)
	{
		roomInfo = room;
		nameLabel.text = room.name;
		playerCountLabel.text = room.playerCount.ToString();
		joinRoomButton.onClick.AddListener(PushJoinRoomButton);
	}

	private void PushJoinRoomButton()
	{
		ConnectionManager.instance.JoinRoom(roomInfo.name);
	}
}
