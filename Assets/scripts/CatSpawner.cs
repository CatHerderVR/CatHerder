using UnityEngine;
using System.Collections;

public class CatSpawner : MonoBehaviour
{
    string[] _catNames = { "SiameseCat", "BlackCat", "OrangeCat", "StripedCat" };

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

    public void SpawnCat()
    {

        GameObject originalCat = Resources.Load<GameObject>( "Cats/" + _catNames[Random.Range(0,_catNames.Length)] );

        GameObject.Instantiate(originalCat, new Vector3(Random.Range(-2,2), 0.05f, Random.Range(-2,2)), Quaternion.identity);
    }
}
