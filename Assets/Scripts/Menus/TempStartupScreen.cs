using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TempStartupScreen : MonoBehaviour {

	NetworkManager networkManager;
	public GameObject clientMenu;
	public GameObject clientButton;

	public void Start() {
		networkManager = GameObject.Find("NetworkGameObject")
			.GetComponent<NetworkManager>();
	}
	// Starts up the game as a client.
	public void StartGameAsClient() {
		// Show the login ui
		clientMenu.SetActive(true);
		clientButton.SetActive(false);
	}

	// Starts up the game as a server.
	public void StartGameAsServer() {
		networkManager.StartServer();

	}
}
