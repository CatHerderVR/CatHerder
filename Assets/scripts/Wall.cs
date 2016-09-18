using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    { 
        GameObject obj = coll.gameObject;

        Debug.Log(string.Concat(gameObject.name, " hit a wall!"));

        Vector3 currentVelocity = obj.GetComponent<Rigidbody>().velocity;
        Vector3 newVelocity = Vector3.ClampMagnitude(-currentVelocity + new Vector3(Random.Range(-.5f, .5f), 0, Random.Range(-.5f, .5f)), currentVelocity.magnitude);

        obj.GetComponent<Rigidbody>().velocity = newVelocity;
    }
}
