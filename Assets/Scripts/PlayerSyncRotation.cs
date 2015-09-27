using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncRotation : NetworkBehaviour {

  [SyncVar]
  private Quaternion syncRotation;
	// Update is called once per frame
	void FixedUpdate() {
    if (isLocalPlayer) {
      HandleRotationInput();
      TransmitRotation();
    } else {
      LerpRotation();
    }
  }

  [Command]
  public void CmdProvideRotationToServer(Quaternion rotation) {
    syncRotation = rotation;
  }

  void LerpRotation() {
    transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, Time.deltaTime * 15);
  }

  [ClientCallback]
  void TransmitRotation() {
    // TODO: only transmit the position if the player has rotated.
    CmdProvideRotationToServer(transform.rotation);
  }

  void HandleRotationInput() {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
  }
}
