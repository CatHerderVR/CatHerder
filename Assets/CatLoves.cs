using UnityEngine;
using System.Collections;

public class CatLoves : MonoBehaviour {

    public string[] Toys = { "Laser", "Yarn" };

    public string CurrentLove;
    public int CurrentDuration;

    public int MaxLoveDurationInSeconds = 50;
    public int MinLoveDurationInSeconds = 10;

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
        this.transform.FindChild("FloatingText").transform.LookAt(GameObject.Find("Main Camera").transform);
        this.transform.FindChild("FloatingText").transform.Rotate(0, 180, 0);
    }
}
