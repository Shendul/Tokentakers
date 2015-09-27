using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncPosition : NetworkBehaviour {

  [SyncVar]
	private Vector3 syncPos;
	[SerializeField] Rigidbody2D myRigidBody;

	private float lerpRate = 30;

	// TODO: move this out of here, and make a call to get it from somewhere.
	private float moveSpeed = 30.0f;

	void FixedUpdate() {
		if (isLocalPlayer) {
			HandleMovementInput();
			TransmitPosition();
		}
	}

	void Update() {
		if (!isLocalPlayer) {
			LerpPosition();
		}
	}

	void LerpPosition() {
		transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
	}

	[Command]
	public void CmdProvidePositionToServer(Vector3 pos) {
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition() {
		// TODO: only transmit the position if the player has moved.
		CmdProvidePositionToServer(transform.position);
	}

	void HandleMovementInput() {
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			myRigidBody.AddForce(new Vector2(-moveSpeed, 0));
		}

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			myRigidBody.AddForce(new Vector2(moveSpeed, 0));
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			myRigidBody.AddForce(new Vector2(0, moveSpeed));
		}

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			myRigidBody.AddForce(new Vector2(0, -moveSpeed));
		}
	}
}
