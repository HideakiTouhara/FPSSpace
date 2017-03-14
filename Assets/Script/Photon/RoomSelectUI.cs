using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectUI : MonoBehaviour {

	[SerializeField] private ConnectionManager connectionManager
	{
		get{return GameManager.instance.ConnectionManager;}
	}
	[SerializeField] private Text connectionStateLabel;
	[SerializeField] private InputField roomNameField;
	[SerializeField] private Button createRoomButton;
	[SerializeField] private RoomInfoPanel roomInfoPanel;

	[SerializeField] private GameObject roomSelectCellPrefab;
	[SerializeField] private Transform scrollPanel;
	private List<RoomSelectCell> cells = new List<RoomSelectCell>();

	private Canvas canvas;

	void Start () 
	{
		canvas = GetComponent<Canvas>();
		createRoomButton.onClick.AddListener(PushCreateButton);
	}

	private void PushCreateButton()
	{
		GameManager.instance.ConnectionManager.CreateRoom(roomNameField.text);
	}
	
	void Update () 
	{
		connectionStateLabel.text = connectionManager.ConnectionState.ToString();
		if(Input.GetKeyDown(KeyCode.M))
		{
			canvas.enabled = !canvas.enabled;
		}
	}

	public void UpdateUIList(RoomInfo[] rooms)
	{
		foreach(var c in cells)
		{
			Destroy(c.gameObject, 0.01f);
		}
		cells = new List<RoomSelectCell>();
		foreach(var r in rooms)
		{
			var obj = Instantiate(roomSelectCellPrefab) as GameObject;
			obj.transform.SetParent(scrollPanel);
			var cell = obj.GetComponent<RoomSelectCell>();
			cell.SetValue(r);
			cells.Add(cell);
		}
	}
}
