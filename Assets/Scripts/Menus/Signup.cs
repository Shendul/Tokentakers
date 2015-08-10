using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Signup : MonoBehaviour {
	private string username = string.Empty;
	private string email = string.Empty;
	private string password = string.Empty;
	private string passwordAgain = string.Empty;

	public void SetUsername(string username) {
		// TODO: Add validation.
		this.username = username;
	}

	public void SetEmail(string email) {
		// TODO: Add validation.
		this.email = email;
	}

	public void SetPassword(string password) {
		// TODO: Add validation.
		this.password = password;
	}

	public void SetPasswordAgain(string passwordAgain) {
		// TODO: Add validation.
		this.passwordAgain = password;
	}


	public void Submit() {
		StartCoroutine(CheckSignup(this.username,
		                           this.email,
		                           this.password,
		                           this.passwordAgain));
	}

	public IEnumerator CheckSignup(string username,
	                               string email,
	                               string password, 
	                               string passwordAgain) {

		Text failureTextObject = GameObject.Find("Signup Failure Text").GetComponent<Text>();

		// Make a web request to signup.
		// TODO: replace the json string handcrafting with something better.
		StringBuilder postData = new StringBuilder();
		postData.Append("{\"username\": \"" + username);
		postData.Append("\",\"email\": \"" + email);
		postData.Append("\",\"password\": \"" + password);
		postData.Append("\",\"passwordAgain\": \"" + passwordAgain + "\"}");
		Debug.Log(postData.ToString());
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");

		byte[] pData = Encoding.ASCII.GetBytes(postData.ToString().ToCharArray());

		WWW signupResponse = new WWW(ApiConstants.SIGNUP_URL, pData, headers);
		yield return signupResponse;
		if (!string.IsNullOrEmpty(signupResponse.error)) {
			Debug.Log(signupResponse.error);
			failureTextObject.text = "FAIL!";
		} else if(!string.IsNullOrEmpty(signupResponse.text)) {
			Debug.LogFormat("repsonse from WWW: {0}", signupResponse.text);
			if (signupResponse.text.Contains("\"isSuccessful\": true")) {
				Debug.Log("Signup success!");
				// Make sure the failure text is empty (hidden).
				failureTextObject.text = "";
				// TODO: Log into game and remove the startup menu
			} else {
				Debug.Log ("Signup Failed!");
				failureTextObject.text = "Username or Email are in use.";
			}

		}
	}
}
