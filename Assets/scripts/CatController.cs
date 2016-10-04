using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Stalking,
        Playing
    }

    public Vector3 initialVelocity;
    public bool TestToysWithViveControllers = true;
    public bool AlwaysChaseLove = false;
    public float WalkSpeed = 0.5f;
    public float RunSpeed = 1;
    public float MinWalkTime = 3f;
    public float MaxWalkTime = 7f;

    State _curState = State.Idle;
    float _curIdleTime = 0;
    float _targetIdleTime = 0;
    CatLoves _catLoves;
    float _yPosition;
    Vector3 _curAccel = Vector3.zero;
    Camera _kittyCam;

    void Start ()
    {
        _catLoves = this.GetComponent<CatLoves>();
        _yPosition = this.transform.position.y;
        _kittyCam = GetComponentInChildren<Camera>();

        //this.GetComponent<Rigidbody>().velocity = initialVelocity;
        //FaceInDirectionOfVelocity();
        //MakeCatWalk();
    }

    void FixedUpdate ()
    {
        Vector3 curVelocity = this.GetComponent<Rigidbody>().velocity;
        Vector3 targetVelocity = curVelocity;

        float distToLove = ( _catLoves.CurrentLove.AttractionTransform.position - _kittyCam.transform.position ).magnitude;
        if(distToLove < 0.2f)
        {
            _curState = State.Playing;
            targetVelocity = Vector3.zero;
            MakeCatPlay();
        }
        else if ( _catLoves.SeesLove || AlwaysChaseLove)
        {
            _curState = State.Stalking;
            //            var lpPos = this.GetComponent<CatLoves>().CurrentLove.GetPosition();
            var lpPos = _catLoves.CurrentLove.AttractionTransform.position;
            var thisPos = this.transform.position;
            Vector3 vDiff = ( lpPos - thisPos );
            targetVelocity = vDiff / vDiff.magnitude * RunSpeed;
            //var anim = this.GetComponentInChildren<Animation>();
            //anim.wrapMode = WrapMode.Loop;
            //anim.CrossFade("Run");
            MakeCatRun();
        }
        else
        {
            _curState = State.Idle;
            _curIdleTime -= Time.deltaTime;
            if( _curIdleTime <= 0 )
            {
                Vector3 randDirection = new Vector3( Random.value, 0, Random.value );
                targetVelocity = randDirection/randDirection.magnitude * WalkSpeed;
                _curIdleTime = Random.Range( MinWalkTime, MaxWalkTime );
            }

            MakeCatWalk();
        }

        //accelerate/decellerate kitty
        //targetVelocity = Vector3.SmoothDamp( curVelocity, targetVelocity, ref _curAccel, 0.3f);
        //targetVelocity = Vector3.Lerp( curVelocity, targetVelocity, 1 );

        this.GetComponent<Rigidbody>().velocity = targetVelocity;

        //if(TestToysWithViveControllers)
        //{
        //    UpdateViveControllersTest();
        //}

        ClampY();

        FaceInDirectionOfVelocity();
   }

    void FaceInDirectionOfVelocity()
    {
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
        if(velocity.magnitude > 0)
            this.transform.rotation = Quaternion.LookRotation(velocity);
    }

    void ClampY()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3( this.GetComponent<Rigidbody>().velocity.x, 0, this.GetComponent<Rigidbody>().velocity.z );

        this.transform.position = new Vector3( this.transform.position.x, _yPosition, this.transform.position.z );
    }

    public void MakeCatRun()
    {
        //this.transform.GetComponentInChildren<Animation>().PlayQueued("Run");

        Animation anim = this.GetComponentInChildren<Animation>();

        anim.CrossFade("Run");
    }

    public void MakeCatPounce()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.CrossFade( "Run");
        anim.wrapMode = WrapMode.Once;
    }

    public void MakeCatWalk()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.CrossFade( "Walk");
        anim.wrapMode = WrapMode.Loop;
    }

    public void MakeCatDance()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.CrossFade( "Dance");
        anim.wrapMode = WrapMode.Loop;

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    public void MakeCatPlay()
    {
        Animation anim = this.GetComponentInChildren<Animation>();

        anim.CrossFade( "Idle03" );
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

        if (leftController != null)
        {
            var controllerLeft = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);

            if (controllerLeft.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) == true)
            {
                string controllerToy = leftController.GetComponentInChildren<TextMesh>().text;
                string currentToy = this.GetComponentInChildren<TextMesh>().text;

                if (controllerToy.ToLower().Equals(currentToy.ToLower()))
                {
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
        }

        if (rightController != null)
        {
            var controllerRight = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);

            if (controllerRight.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) == true)
            {
                string controllerToy = rightController.GetComponentInChildren<TextMesh>().text;
                string currentToy = this.GetComponentInChildren<TextMesh>().text;

                if (controllerToy.ToLower().Equals(currentToy.ToLower()))
                {
                    var lpPos = new Vector3(controllerRight.transform.pos.x, 0, controllerRight.transform.pos.z);
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
                    var lpPos = new Vector3(controllerRight.transform.pos.x, 0, controllerRight.transform.pos.z);
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
    
    //void OnTriggerEnter(Collider coll)
    //{ 
    //    if (coll.gameObject.layer == 10)
    //    {
    //        MakeCatDance();
    //    }
    //}
}
