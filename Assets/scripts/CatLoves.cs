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

    string _catName;
    CatToy[] _toys;
    CatToy _currentLove;
    bool _seesLove;
    Camera _kittyCam;

    public string CatName { get { return _catName; } set { _catName = value; } }
    public CatToy CurrentLove { get { return _currentLove; } }
    public bool SeesLove { get { return _seesLove; } }

    void Start ()
    {
        _toys = GameObject.FindObjectsOfType<CatToy>();
        _kittyCam = GetComponentInChildren<Camera>();

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
        //check if love is in view
        Vector3 screenPoint = _kittyCam.WorldToViewportPoint( CurrentLove.transform.position );
        _seesLove = screenPoint.z < _kittyCam.farClipPlane && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

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
            floatingText.GetComponent<TextMesh>().text = _catName + " | " + _currentLove.ToyName;
        }

        Invoke("SetLove", CurrentDuration);
    }

    public void TextFacesCamera()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
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
