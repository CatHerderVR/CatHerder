﻿using UnityEngine;
using System.Collections;

public class CatSpawner : MonoBehaviour
{
    string[] _catNames = { "Siamese", "Black", "Orange", "Striped" };

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject rightController = GameObject.Find("Controller (right)");
        GameObject leftController = GameObject.Find( "Controller (left)" );

        if( rightController != null)
        {
            var controllerRight = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);

            if (controllerRight.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) == true)
            {
                SpawnCat();
            }
        }

        if( leftController != null )
        {
            var controllerLeft = SteamVR_Controller.Input( (int)leftController.GetComponent<SteamVR_TrackedObject>().index );

            if( controllerLeft.GetPressUp( SteamVR_Controller.ButtonMask.Touchpad ) == true )
            {
                SpawnCat();
            }
        }
    }

    public void SpawnCat()
    {

        //        GameObject originalCat = Resources.Load<GameObject>( "Cats/" + _catNames[Random.Range(0,_catNames.Length)] );
        //        GameObject originalCat = Resources.Load<GameObject>( "Cats/BlackCat" );

        GameObject kitty = GameObject.Instantiate( Resources.Load<GameObject>( "Kitty/Kitty" ), new Vector3(Random.Range(-2,2), 0.05f, Random.Range(-2,2)), Quaternion.identity) as GameObject;

        //randomly select skin
        kitty.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<Material>( "Kitty/" + _catNames[Random.Range( 0, _catNames.Length )] );
    }
}
