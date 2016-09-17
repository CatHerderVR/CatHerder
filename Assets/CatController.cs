using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

    public Vector3 initialVelocity;

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<Rigidbody>().velocity = initialVelocity;
        FaceInDirectionOfVelocity();
	}
	
	// Update is called once per frame
	void Update ()
    {
        FaceInDirectionOfVelocity();
        ClampY();
    }

    void FaceInDirectionOfVelocity()
    {
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;

        this.transform.rotation = Quaternion.LookRotation(velocity);
    }

    void ClampY()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x, 0, this.GetComponent<Rigidbody>().velocity.z);

        this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }
}
