using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		InitGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitGame() {
		//Do some stuff to init the game here.
	
	}
}
