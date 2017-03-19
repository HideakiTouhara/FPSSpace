using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager> {

	public static int playerWhoIsIt = 0;
	private PhotonView ScenePhotonView;

	[SerializeField] private Transform[] spawnPoints;
	public Transform[] SpawnPoints { get{return spawnPoints;}}

	private bool gameStart = false;
	private int standbyedCount, entryOrder;
	readonly string standbyedCountKey = "StandbyedCount";

	[SerializeField] private GameObject preparationCamera;

	// Use this for initialization
	private void Start () {
		var hash = PhotonNetwork.room.customProperties;
		if(hash.ContainsKey(standbyedCountKey))
		{
			int currentReadyCount = (int)hash[standbyedCountKey];
		    print(currentReadyCount);
			entryOrder = currentReadyCount;
			currentReadyCount += 1;
			hash[standbyedCountKey] = currentReadyCount;
			PhotonNetwork.room.SetCustomProperties(hash);
//			print(currentReadyCount + "/" + PhotonNetwork.room.playerCount);
			if(currentReadyCount == PhotonNetwork.room.playerCount - 1)
			{
				CreateCharacter();
				gameStart = true;
			}
		}
		else
		{
			hash.Add(standbyedCount, 1);
			PhotonNetwork.room.SetCustomProperties(hash);
		}
	}

//	 Update is called once per frame
	void Update () {
		if(!gameStart)
		{
			var hash = PhotonNetwork.room.customProperties;
			int currentReadyCount = (int)hash[standbyedCountKey];
//			print(currentReadyCount + "/" + PhotonNetwork.room.playerCount);
			if(currentReadyCount == PhotonNetwork.room.playerCount)
			{
				CreateCharacter();
				gameStart = true;
			}
		}
	}

	private void CreateCharacter()
	{
	    preparationCamera.gameObject.SetActive(false);
		GameObject character = PhotonNetwork.Instantiate("FPSController", spawnPoints[entryOrder].position, spawnPoints[entryOrder].rotation, 0);
		character.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
		character.GetComponentInChildren<AudioListener>().enabled = true;
	    var playerController = character.GetComponent<PlayerController>();
	    playerController.CameraStart();
	}
}
