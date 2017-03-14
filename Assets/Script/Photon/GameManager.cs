using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[SerializeField] ConnectionManager _connectionManager;
	public ConnectionManager ConnectionManager {get {return _connectionManager;}}

	void Start () 
	{
		if(instance == null)
		{
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

	public void LoadScene(Const.Scene scene)
	{
		SceneManager.LoadScene(scene.ToString());
	}
	

}
