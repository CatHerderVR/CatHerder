using UnityEngine;
using System.Collections;

public class CatLoves : MonoBehaviour {

    public string[] Toys = { "Laser", "Fish", "Yarn", "Robotuna" };

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
	}

    public void SetLove()
    {
        //Choose a Love
        int index = Random.Range(0, Toys.Length);
        CurrentLove = Toys[index];

        //Choose duration
        CurrentDuration = Random.Range(MinLoveDurationInSeconds, MaxLoveDurationInSeconds);

        this.transform.FindChild("FloatingText").GetComponent<TextMesh>().text = CurrentLove;

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
}
