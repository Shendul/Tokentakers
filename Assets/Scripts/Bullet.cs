﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour {
	private float moveSpeed = 20.0f;
	public Vector3 direction;
	public Player owner;

	void Update() {
		if (!base.isServer) {
			return;
		}

		transform.position += direction * Time.deltaTime * moveSpeed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Player otherPlayer = other.gameObject.GetComponent<Player>();
		if (otherPlayer == null)
			return;
		if (otherPlayer.Equals(owner)) {
			//Debug.Log("OH SHIZ I SHOT MYSELFS");
		} else {
			Debug.Log("YAY I SHOT SOMEONE ELSE: " + otherPlayer.playerName);
		}
	}
}
