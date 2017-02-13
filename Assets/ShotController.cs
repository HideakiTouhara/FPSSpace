using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	int bullet = 30;
	int bulletBox = 150;
	float shotInterval = 0.0f;
	public float coolTime = 1.0f;

	public GameObject sparkle;
	public GameObject gunPoint;
	AudioSource audioSource;
	public AudioClip audioClip;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && shotInterval > coolTime) {
			shotInterval = 0;
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			GameObject sparkele3 = Instantiate(sparkle);
			sparkele3.transform.position = gunPoint.transform.position;
			sparkele3.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			Destroy(sparkele3, 0.1f);
			audioSource.PlayOneShot(audioClip);
			bullet -= 1;
			if(Physics.Raycast(ray, out hit) && bullet != 0) {
				GameObject sparkele2 = Instantiate(sparkle);
				sparkele2.transform.position = hit.point;
				Destroy(sparkele2, 0.1f);
			}
		}
		shotInterval += Time.deltaTime;
		
	}
}
