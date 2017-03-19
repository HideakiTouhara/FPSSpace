using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	public int startBullet = 30;
	public int bullet;
	public int bulletBox = 150;
	float shotInterval = 0.0f;
	[SerializeField] private float coolTime = 0.1f;
	[SerializeField] private int startTargetLife = 5;
	int targetLife;

	public GameObject sparkle;
	public GameObject gunPoint;
	AudioSource audioSource;
	[SerializeField] private AudioClip audioClip;
	[SerializeField] private AudioClip reloadSound;
	[SerializeField] private TargetController targetController;

	[SerializeField] private GameObject headMarker;
	public int score = 0;

	[SerializeField] GameObject snipe;
	bool isSnipe = false;
	[SerializeField] private Camera camerafv;

	[SerializeField] private UnityStandardAssets.Characters
		.FirstPerson.FirstPersonController firstPersonController;
	private Const.PlayerState playerState = Const.PlayerState.Live;

	public int hitPoint = 100;
	private int hitPointFull = 100;

	[SerializeField] private GameObject[] resPawnPoint;

	// Use this for initialization
	void Start () {
		targetLife = startTargetLife;
		audioSource = GetComponent<AudioSource>();
		bullet = startBullet;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0) && shotInterval > coolTime) {
			shotInterval = 0;
			Ray ray = new Ray(camerafv.transform.position, camerafv.transform.forward);
			RaycastHit hit;
			GameObject sparkele3 = Instantiate(sparkle);
			sparkele3.transform.position = gunPoint.transform.position;
			sparkele3.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			Destroy(sparkele3, 0.1f);
			audioSource.PlayOneShot(audioClip);
			bullet -= 1;
			if(Physics.Raycast(ray, out hit) && bullet != 0) {
				GameObject sparkele2 = Instantiate(sparkle);
				sparkele2.transform.position = hit.point + new Vector3(0, 0, -0.3f);
				sparkele2.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				Destroy(sparkele2, 0.1f);

				if(hit.collider.name == "pCube1" || hit.collider.name == "pCylinder1") {
					targetLife -= 1;
					CalcScore(hit.point);
				}
				if(targetLife == 0) {
					targetController.brokenTarget();
					targetLife = startTargetLife;
				}
				if(hit.transform.tag == "Player") {
					var myView = GetComponent<PhotonView>();
					var otherView = hit.transform.GetComponent<PhotonView>();
					int damage = 20;
					if(otherView.ownerId == PhotonNetwork.player.ID) {
						otherView.RPC("ReceiveDamage", PhotonPlayer.Find(otherView.ownerId), damage);
					}
				}
			}
		}
		shotInterval += Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.R)) {
			Reload();
		}

		if(Input.GetMouseButtonDown(1)) {
			if(isSnipe == false) {
				camerafv.fieldOfView = 30.0f;
				snipe.SetActive(true);
				isSnipe = true;
			} else {
				camerafv.fieldOfView = 64.4f;
				snipe.SetActive(false);
				isSnipe = false;
			}
		}
	}

    public void CameraStart()
    {
        camerafv.enabled = true;
    }

	void Reload() {
		if(bullet < startBullet && bulletBox > 0) {
			int bulletDiff = startBullet - bullet;
			audioSource.PlayOneShot(reloadSound);
			if(bulletBox > bulletDiff) {
				bulletBox -= bulletDiff;
				bullet += bulletDiff;
			} else {
				bulletBox = 0;
				bullet += bulletBox;
			}
		}
	}

	void CalcScore(Vector3 hitPoint) {
		float diff = (headMarker.transform.position - hitPoint).magnitude;
		if(diff <= 0.3f) {
			score += 100;
		} else if(diff <= 0.5f) {
			score += 50;
		} else {
			score += 30;
		}
	}

	[PunRPC]
	void ReceiveDamage(int damage) {
		hitPoint -= damage;
		UIManager.instance.ReceiveDamage();
		if(hitPoint < 0) {
			print("Dead");
			Respawn();
		}
	}

	private void Respawn() {
		hitPoint = hitPointFull;
		transform.position = resPawnPoint[Random.Range(0, 3)].transform.position;
		transform.rotation = resPawnPoint[Random.Range(0, 3)].transform.rotation;
	}
}
