using UnityEngine;
using System.Collections;

public class CatLoves : MonoBehaviour {

    public string[] Toys = { "Laser", "Fish", "Yarn", "Robotuna" };
    public Texture[] ToyImages;

    public string CurrentLove;
    public int CurrentDuration;

    public int MaxLoveDurationInSeconds = 15;
    public int MinLoveDurationInSeconds = 5;

    // Use this for initialization
    void Start ()
    {
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
        int index = Random.Range(0, Toys.Length);
        CurrentLove = Toys[index];

        //Choose duration
        CurrentDuration = Random.Range(MinLoveDurationInSeconds, MaxLoveDurationInSeconds);

        this.transform.FindChild("FloatingText").GetComponent<TextMesh>().text = CurrentLove;
        this.transform.FindChild("Quad").GetComponent<MeshRenderer>().material.mainTexture = ToyImages[index];
        
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

        this.transform.FindChild("FloatingText").transform.LookAt(camera.transform);
        this.transform.FindChild("FloatingText").transform.Rotate(0, 180, 0);
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
