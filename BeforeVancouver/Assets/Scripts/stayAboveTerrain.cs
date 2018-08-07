using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayAboveTerrain : MonoBehaviour
{
    public float yAddition = 0.5f;
    private bool newTerrain;
    private void LateUpdate()
    {
        newTerrain = false;
        #if UNITY_STANDALONE_WIN
            if (UnityEngine.XR.XRSettings.enabled)
            {
                OVRInput.Button oculusTouchButtonA = OVRInput.Button.PrimaryIndexTrigger;
                OVRInput.Controller activeController = OVRInput.GetActiveController();
                if (OVRInput.GetUp(oculusTouchButtonA))
                {
                    newTerrain = true;
                }
            }
        #endif
        if (Input.GetKeyUp(KeyCode.Y)) newTerrain = true;

        if (newTerrain) { 
            Vector3 pos = transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + yAddition;
            transform.position = pos;
        }
    }
}


