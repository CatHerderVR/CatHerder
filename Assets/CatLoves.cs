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

    private static Func<Vector3> LaserPointerToy(string name)
    {
        return () =>
        {
            var lpObj = GameObject.Find(name);
            return lpObj.transform.FindChild("EndFlare").position;
        };
        }

    // Use this for initialization
    void Start ()
    {
        Toys = new Func<Vector3>[] {
            ObjToy("Fish"), ObjToy("Yarn"), ObjToy("Robotuna"),
            LaserPointerToy("LaserPointer1")
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
<<<<<<< HEAD
    //    this.transform.FindChild("FloatingText").transform.LookAt(GameObject.Find("Main Camera").transform);
    //    this.transform.FindChild("FloatingText").transform.Rotate(0, 180, 0);
=======
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
>>>>>>> 69728aec5c6677e3b87a371248d9836d0f3a4773
    }
}
