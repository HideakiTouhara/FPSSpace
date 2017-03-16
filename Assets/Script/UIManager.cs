using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] private Text time;
	[SerializeField] private Text pt;
	[SerializeField] private Text bullet;
	[SerializeField] private Text bulletBox;

	[SerializeField] PlayerController playerController;
	[SerializeField] float remainingTime = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		remainingTime -= Time.deltaTime;
		string remainingTime2 = remainingTime.ToString("F1");
		time.text = "Time: " + remainingTime2 + "s";
		pt.text = "Pt: " + playerController.score;
		bullet.text = "Bullet: " + playerController.bullet + "/" + playerController.startBullet;
		bulletBox.text = "BulletBox: " + playerController.bulletBox;
		
	}
}
