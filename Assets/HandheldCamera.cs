using UnityEngine;
using System.Collections;

public class HandheldCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        Camera thisCamera = this.GetComponent<Camera>();

        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);

        this.transform.position = cursorPosition;
    }


}
