using UnityEngine;
using System.Collections;

public class temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    public void Update()
    {
        if( Input.GetKeyDown( KeyCode.LeftArrow ) )
            transform.position += new Vector3( 0.1f, 0, 0 );
        if( Input.GetKeyDown( KeyCode.RightArrow ) )
            transform.position += new Vector3( -0.1f, 0, 0 );
    }
}
