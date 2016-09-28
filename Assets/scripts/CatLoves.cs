using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

public class CatLoves : MonoBehaviour
{
    //public Toy[] Toys; 

    //public Toy CurrentLove;
    public int CurrentDuration;

    public int MaxLoveDurationInSeconds = 15;
    public int MinLoveDurationInSeconds = 5;

    CatToy[] _toys;
    CatToy _currentLove;

    public CatToy CurrentLove { get { return _currentLove; } }

    // Use this for initialization
    void Start ()
    {
        _toys = GameObject.FindObjectsOfType<CatToy>();

//        Toys = new Toy[] {
//            new NamedObjToy("EndFlare")
////            new NamedObjToy("Fish"), new NamedObjToy("Yarn"), new NamedObjToy("Robotuna"),
//            //new PathObjToy("Laser", "[CameraRig]", new[] { "Controller (right)", "LaserPointerContainer", "LaserPointer1", "EndFlare" })
//        };
        SetLove();
	}
	
	// Update is called once per frame
	void Update ()
    {
        TextFacesCamera();
        ImageFacesCamera();
	}

    public void SetLove()
    {
        //Choose a Love
        int index = Random.Range(0, _toys.Length);
        _currentLove = _toys[index];

        //Choose duration
        CurrentDuration = Random.Range(MinLoveDurationInSeconds, MaxLoveDurationInSeconds);

        var floatingText = this.transform.FindChild("FloatingText");
        if(floatingText != null)
        {
            floatingText.GetComponent<TextMesh>().text = _currentLove.ToyName;
        }

        Invoke("SetLove", CurrentDuration);
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

        var floatingText = this.transform.FindChild("FloatingText");
        if (floatingText != null)
        {
            this.transform.FindChild("FloatingText").transform.LookAt(camera.transform);
            this.transform.FindChild("FloatingText").transform.Rotate(0, 180, 0);
        }        
    }

    public void ImageFacesCamera()
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

        this.transform.FindChild("Quad").transform.LookAt(camera.transform);
        this.transform.FindChild("Quad").transform.Rotate(0, 180, 0);
    }

}
