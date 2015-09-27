using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

	public GameObject projectilePrefab;
	public string playerName;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer){
			Vector3 PlayerPOS = transform.position;
			GameObject.Find("Main Camera").transform.position =
				new Vector3(PlayerPOS.x, PlayerPOS.y, -10);

			if (Input.GetMouseButtonDown(0)) {
				CmdFire();
			}
		}
	}

	[Command]
	public void CmdFire() {
		GameObject projectile = (GameObject)GameObject
			.Instantiate(projectilePrefab, transform.position, transform.rotation);
		Bullet bullet = projectile.GetComponent<Bullet>();
		bullet.direction = transform.up;
		bullet.owner = this;
		//TODO: Get the lifetime of the projectile from the projectile itself.
		Destroy(projectile, 1.0f);
		NetworkServer.Spawn(projectile);
	}

	[Command]
	public void CmdKillPlayer() {
		Debug.Log("Killing Player: " + playerName);
		// TODO: Kill the player.
	}

	[Command]
	public void CmdSetPlayerName(string name) {
		Debug.Log("Player name set");
		playerName = name;
	}
}
