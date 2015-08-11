using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Login : MonoBehaviour {
	private string username = string.Empty;
	private string password = string.Empty;
	
	public NetworkManager networkManager;

	public void SetUsername(string username) {
		// TODO: Add validation.
		this.username = username;
	}

	public void SetPassword(string password) {
		// TODO: Add validation.
		this.password = password;
	}


	public void Submit() {
		StartCoroutine(CheckLogin(this.username, this.password));
	}

	public IEnumerator CheckLogin(string username, string password) {
		
		Text failureTextObject = GameObject.Find("Login Failure Text").GetComponent<Text>();

		// Make a web request to login.
		// TODO: replace the json string handcrafting with something better.
		StringBuilder postData = new StringBuilder();
		postData.Append("{\"username\":\"" + username);
		postData.Append("\",\"password\": \"" + password + "\"}");
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");

		byte[] pData = Encoding.ASCII.GetBytes(postData.ToString().ToCharArray());

		WWW loginResponse = new WWW(ApiConstants.LOGIN_URL, pData, headers);
		yield return loginResponse;
		if (!string.IsNullOrEmpty(loginResponse.error)) {
			Debug.Log(loginResponse.error);
			failureTextObject.text = "FAIL!";
		} else if(!string.IsNullOrEmpty(loginResponse.text)) {
			//Debug.LogFormat("repsonse from WWW: {0}", loginResponse.text);
			if (loginResponse.text.Contains("\"isSuccessful\": true")) {
				//Debug.Log("Login success!");
				// Make sure the failure text is empty (hidden).
				failureTextObject.text = "";
				// TODO: remove the startup menu and log into the game.
				networkManager.StartClient();
			} else {
				//Debug.Log ("Login Failed!");
				failureTextObject.text = "Username or Pass are incorrect.";
			}

		}
	}
}
