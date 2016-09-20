using UnityEngine;
using System.Collections;

public class CatSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject rightController = GameObject.Find("Controller (right)");

        if (rightController != null)
        {
            var controllerRight = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);

            if (controllerRight.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) == true)
            {
                SpawnCat();
            }
        }
    }

void SpawnCat()
    {
        string[] catNames = { "SiameseCat" };

        GameObject originalCat = GameObject.Find("SiameseCat");

        GameObject.Instantiate(originalCat, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
