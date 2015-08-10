using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TempStartupScreen : MonoBehaviour {

	NetworkManager networkManager;
	public void Start() {
		networkManager = GameObject.Find("NetworkGameObject")
			.GetComponent<NetworkManager>();
	}
	// Starts up the game as a client.
	public void StartGameAsClient() {
		networkManager.StartClient();
	}

	// Starts up the game as a server.
	public void StartGameAsServer() {
		networkManager.StartServer();

	}
}
