using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : SingletonMonoBehaviour<GameManager> {

	[SerializeField] ConnectionManager _connectionManager;
	public ConnectionManager ConnectionManager {get {return _connectionManager;}}

	void Start () 
	{
		DontDestroyOnLoad(gameObject);
	}

	public void LoadScene(Const.Scene scene)
	{
		SceneManager.LoadScene(scene.ToString());
	}
	

}
