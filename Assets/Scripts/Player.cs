using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

	public GameObject projectilePrefab;

	public string playerName;

	public enum NudgeDir
	{
		Up,
		Down,
		Left,
		Right
	}

	public float moveSpeed = 20.0f;

	[ClientCallback]
	void Update() {
		if (!isLocalPlayer)
			return;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		CmdRotatePlayer(mousePos);

		if (Input.GetMouseButtonDown(0)) {
			CmdFire();
		}
		HandleMovementInput();
	}

	void HandleMovementInput() {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			CmdNudge(NudgeDir.Left);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			CmdNudge(NudgeDir.Right);
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			CmdNudge(NudgeDir.Up);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			CmdNudge(NudgeDir.Down);
		}
	}


	[Command]
	public void CmdNudge(NudgeDir direction) {
		switch (direction) {
			case NudgeDir.Up:
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed));
			break;
		case NudgeDir.Right:
			GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, 0));
			break;
		case NudgeDir.Down:
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -moveSpeed));
			break;
		case NudgeDir.Left:
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed, 0));
			break;
		}
	}

	[Command]
	public void CmdRotatePlayer(Vector3 mousePos) {
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
	}

	[Command]
	public void CmdFire() {
		GameObject projectile = (GameObject)GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		Bullet bullet = projectile.GetComponent<Bullet>();
		bullet.direction = transform.up;
		bullet.owner = this;
		Destroy(projectile, 2.0f);
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
