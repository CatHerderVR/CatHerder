using UnityEngine;
using System.Collections;

public class LaserPointerController : MonoBehaviour {
    float radius = 10f;
    Vector2 center;
    float theta = 0f;
    float periodInSec = 5.0f;

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


    //void OnTriggerEnter(Collider coll)
    //{
    //    string toyName = coll.gameObject.transform.FindChild("FloatingText").GetComponent<TextMesh>().text;

    //    //if cat love is equal to object text, change direction
    //    if (toyName != null)
    //    {
    //        if (toyName.ToLower().Equals(coll.gameObject.transform.FindChild("FloatingText").GetComponent<TextMesh>().text.ToLower()))
    //        {
    //            //change direction
    //            MakeCatRun();

    //            Vector3 newDirection = coll.gameObject.transform.position - this.gameObject.transform.position;

    //            newDirection = new Vector3(newDirection.x, 0, newDirection.z);

    //            this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(newDirection, 3);
    //        }
    //    }
    //}

    //void OnTriggerExit(Collider coll)
    //{
    //    string toyName = coll.gameObject.transform.FindChild("FloatingText").GetComponent<TextMesh>().text;
    //    string toyLove = this.gameObject.GetComponent<CatLoves>().CurrentLove;

    //    //change direction
    //    MakeCatWalk();

    //    //if cat love is equal to object text, change direction
    //    if (toyName != null)
    //    {
    //        if (toyName.ToLower().Equals(toyLove.ToLower()))
    //        {
    //            Vector3 newDirection = coll.gameObject.transform.position - this.gameObject.transform.position;

    //            newDirection = new Vector3(newDirection.x, 0, newDirection.z);

    //            this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(newDirection, 3);
    //        }
    //    }
    //}
}
