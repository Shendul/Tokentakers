using UnityEngine;
using System.Collections;

public class StartupMenuManager : MonoBehaviour {

	public GameObject signInCanvas;
	public GameObject createAccountCanvas;

	public void Start() {
		// We show the sign in canvas by default.
		createAccountCanvas.SetActive(false);
	}

	public void LoadAccountGUI() {
		Debug.Log("LoadAccountGUI");
		// Hide the Sign In Canvas.
		signInCanvas.SetActive(false);
		// Show the Create Account Canvas.
		createAccountCanvas.SetActive(true);
	}
}
