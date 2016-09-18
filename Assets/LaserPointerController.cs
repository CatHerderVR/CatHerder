using UnityEngine;
using System.Collections;

public class LaserPointerController : MonoBehaviour {
    float radius = 9f;
    Vector2 center;
    float theta = 0f;
    float periodInSec = 2.0f;

	// Use this for initialization
	void Start () {
       center  = new Vector2(this.transform.localPosition.x, this.transform.localPosition.z) - new Vector2(radius, 0f);
    }

    // Update is called once per frame
    void Update () {
        theta += 2 * Mathf.PI * Time.deltaTime / periodInSec;
        var pos2 = center + new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * radius;
        this.transform.localPosition = new Vector3(pos2.x, this.transform.localPosition.y, pos2.y);
	}
}
