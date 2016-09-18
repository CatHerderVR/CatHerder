using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    
	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void FixedUpdate () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are touch holding the trigger");
        }
	}
}
