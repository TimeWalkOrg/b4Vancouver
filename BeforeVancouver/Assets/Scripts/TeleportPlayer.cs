using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {
    public Transform[] teleportLocations;
    private int currentLocationIndex = 0;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.T))
            TeleportToLocation();
#if UNITY_STANDALONE_WIN
        if (UnityEngine.XR.XRSettings.enabled)
        {
            OVRInput.Button oculusTouchButtonC = OVRInput.Button.One; // Oculus GO: Click dpad button for Teleport
            OVRInput.Controller activeController = OVRInput.GetActiveController();
            if (OVRInput.GetUp(oculusTouchButtonC))
            {
                TeleportToLocation();
            }
        }
#endif
    }

    private void TeleportToLocation()
    {
		currentLocationIndex = currentLocationIndex >= teleportLocations.Length ? 0 : currentLocationIndex + 1;
        transform.position = teleportLocations[currentLocationIndex].position;
    }
}
