using UnityEngine;
using System.Collections;

public class CatToy : MonoBehaviour
{
    public string ToyName;
    public Transform AttractionTransform;

/*
    public string[] Toys;

    private int index = 0;

	// Use this for initialization
	void Start ()
    {
        SetToy();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var device = SteamVR_Controller.Input((int)this.transform.parent.GetComponent<SteamVR_TrackedObject>().index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) == true)
        {
            SetToy();
            TextFacesCamera();
        }
    }

    void SetToy()
    {
        this.GetComponent<TextMesh>().text = Toys[index];

        index++;

        if (index >= Toys.Length)
        {
            index = 0;
        }
    }

    public void TextFacesCamera()
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (camera == null)
        {
            camera = GameObject.Find("[CameraRig]");
        }
        if (camera == null)
        {
            return;
        }

        this.transform.FindChild("FloatingText").transform.LookAt(camera.transform);
        this.transform.FindChild("FloatingText").transform.Rotate(0, 180, 0);
    }
    */
}
