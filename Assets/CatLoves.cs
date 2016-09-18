using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

public class CatLoves : MonoBehaviour {
    public Func<Vector3>[] Toys; 

    public Func<Vector3> CurrentLove;
    public int CurrentDuration;

    public int MaxLoveDurationInSeconds = 15;
    public int MinLoveDurationInSeconds = 5;

    private static Func<Vector3> ObjToy(string name)
    {
        return () =>
        {
            var lpObj = GameObject.Find(name);
            return lpObj.transform.position;
        };
    }

    private static Func<Vector3> PathToy(string rootObjName, string[] objPath)
    {
        return () =>
        {
            var lpObj = GameObject.Find(rootObjName);
            var currTransform = lpObj.transform;
            foreach(var pathComponent in objPath)
            {
                currTransform = currTransform.FindChild(pathComponent);
            }
            return currTransform.position;
        };
        }

    // Use this for initialization
    void Start ()
    {
        Toys = new Func<Vector3>[] {
            ObjToy("Fish"), ObjToy("Yarn"), ObjToy("Robotuna"),
            PathToy("[CameraRig]", new[] { "Controller (left)", "LaserPointerContainer", "LaserPointer1", "EndFlare" })
        };
        SetLove();
	}
	
	// Update is called once per frame
	void Update ()
    {
        TextFacesCamera();
	}

    public void SetLove()
    {
        //Choose a Love
        int index = Random.Range(0, Toys.Length);
        CurrentLove = Toys[index];

        //Choose duration
        CurrentDuration = Random.Range(MinLoveDurationInSeconds, MaxLoveDurationInSeconds);

       // this.transform.FindChild("FloatingText").GetComponent<TextMesh>().text = CurrentLove;

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
}
