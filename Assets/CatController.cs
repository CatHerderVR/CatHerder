﻿using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

    public Vector3 initialVelocity;
    private float yPosition;

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<Rigidbody>().velocity = initialVelocity;
        FaceInDirectionOfVelocity();
        this.yPosition = this.transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //TODO local vs.world matrix?
        var lpPos = this.GetComponent<CatLoves>().CurrentLove();
        var thisPos = this.transform.position;
        this.GetComponent<Rigidbody>().velocity = (lpPos - thisPos) / 2.5f;
        FaceInDirectionOfVelocity();
        var anim = this.GetComponentInChildren<Animation>();
        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("Run");

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

         this.transform.position = new Vector3(this.transform.position.x, yPosition, this.transform.position.z);
    }
}
