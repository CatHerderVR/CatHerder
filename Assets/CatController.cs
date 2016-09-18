using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

    public Vector3 initialVelocity;
    private float yPosition;

    public bool TestToysWithViveControllers = true;
    public bool AlwaysChaseLove = true;

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<Rigidbody>().velocity = initialVelocity;
        FaceInDirectionOfVelocity();
        this.yPosition = this.transform.localPosition.y;
        MakeCatWalk();
	}
	
	// Update is called once per frame
	void Update ()
    {
        FaceInDirectionOfVelocity();

        if (AlwaysChaseLove)
        {
            var lpObj = GameObject.Find(this.GetComponent<CatLoves>().CurrentLove);
            // TODO local vs.world matrix?
            var lpPos = lpObj.transform.localPosition;
            var thisPos = this.transform.localPosition;
            this.GetComponent<Rigidbody>().velocity = (lpPos - thisPos) / 2.5f;
            FaceInDirectionOfVelocity();
            var anim = this.GetComponentInChildren<Animation>();
            anim.wrapMode = WrapMode.Loop;
            anim.CrossFade("Run");
        }

        if(TestToysWithViveControllers)
        {
            UpdateViveControllersTest();
        }

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

        this.transform.position = new Vector3(this.transform.position.x, yPosition, this.transform.position.z);
    }

    public void MakeCatRun()
    {
        this.transform.GetComponentInChildren<Animation>().PlayQueued("Run");

        Animation anim = this.GetComponentInChildren<Animation>();

        anim.Play("Run");
    }

    public void MakeCatPounce()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.Play("Run");
        anim.wrapMode = WrapMode.Once;
    }

    public void MakeCatWalk()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.Play("Walk");
        anim.wrapMode = WrapMode.Loop;
    }

    public void MakeCatDance()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.Play("Dance");
        anim.wrapMode = WrapMode.Loop;

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void MakeCatWalkRandom()
    {
        //    Vector3 velocity = Vector3.ClampMagnitude(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), 1);
        MakeCatWalk();
    }

    public void UpdateViveControllersTest()
    {
        GameObject leftController = GameObject.Find("Controller (left)");
        GameObject rightController = GameObject.Find("Controller (right)");

        if (leftController == null || rightController == null)
        {
            return;
        }

        var controllerLeft = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
        var controllerRight = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);

        if (controllerLeft.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) == true)
        {
            string controllerToy = leftController.GetComponentInChildren<TextMesh>().text;
            string currentToy = this.GetComponentInChildren<TextMesh>().text;

            if (controllerToy.ToLower().Equals(currentToy.ToLower()))
            {
                // var lpObj = GameObject.Find("LaserPointerSurrogate");
                // TODO local vs.world matrix?
                var lpPos = new Vector3(controllerLeft.transform.pos.x, 0, controllerLeft.transform.pos.z);
                var thisPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude((lpPos - thisPos) / 2.5f, 1);
                FaceInDirectionOfVelocity();
                var anim = this.GetComponentInChildren<Animation>();
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Run");
            }
        }

        if (controllerLeft.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) == true)
        {
            string controllerToy = leftController.GetComponentInChildren<TextMesh>().text;
            string currentToy = this.GetComponentInChildren<TextMesh>().text;

            if (controllerToy.ToLower().Equals(currentToy.ToLower()))
            {
                var lpPos = new Vector3(controllerLeft.transform.pos.x, 0, controllerLeft.transform.pos.z);
                var thisPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude((thisPos - lpPos) / 2.5f, 1);
                FaceInDirectionOfVelocity();
                var anim = this.GetComponentInChildren<Animation>();
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Walk");
            }
        }

        if (controllerRight.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) == true)
        {
            string controllerToy = rightController.GetComponentInChildren<TextMesh>().text;
            string currentToy = this.GetComponentInChildren<TextMesh>().text;

            if (controllerToy.ToLower().Equals(currentToy.ToLower()))
            {
                // var lpObj = GameObject.Find("LaserPointerSurrogate");
                // TODO local vs.world matrix?
                var lpPos = new Vector3(controllerRight.transform.pos.x, 0, controllerLeft.transform.pos.z);
                var thisPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude((lpPos - thisPos) / 2.5f, 1);
                FaceInDirectionOfVelocity();
                var anim = this.GetComponentInChildren<Animation>();
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Run");
            }
        }

        if (controllerRight.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) == true)
        {
            string controllerToy = rightController.GetComponentInChildren<TextMesh>().text;
            string currentToy = this.GetComponentInChildren<TextMesh>().text;

            if (controllerToy.ToLower().Equals(currentToy.ToLower()))
            {
                var lpPos = new Vector3(controllerRight.transform.pos.x, 0, controllerLeft.transform.pos.z);
                var thisPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude((thisPos - lpPos) / 2.5f, 1);
                FaceInDirectionOfVelocity();
                var anim = this.GetComponentInChildren<Animation>();
                anim.wrapMode = WrapMode.Loop;
                anim.CrossFade("Walk");
            }
        }
    }
}
