using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager> {

	[SerializeField] private Text time;
	[SerializeField] private Text pt;
	[SerializeField] private Text bullet;
	[SerializeField] private Text bulletBox;

	[SerializeField] PlayerController playerController;
	[SerializeField] float remainingTime = 100.0f;

//	[SerializeField] private GameObject damagedImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		remainingTime -= Time.deltaTime;
		string remainingTime2 = remainingTime.ToString("F1");
		time.text = "Time: " + remainingTime2 + "s";
		if(playerController == null) return;
	    pt.text = "Pt: " + playerController.hitPoint;
	    bullet.text = "Bullet: " + playerController.bullet + "/" + playerController.startBullet;
	    bulletBox.text = "BulletBox: " + playerController.bulletBox;
	}

    public void SetMyPlayer(PlayerController player)
    {
        playerController = player;
    }

//	public void ReceiveDamage() {
//		damagedImage.SetActive(true);
//		Invoke("ResetReceiveDamage", 1.0f);
//	}
//
//	void ResetReceiveDamage() {
//		damagedImage.SetActive(false);
//	}
}
