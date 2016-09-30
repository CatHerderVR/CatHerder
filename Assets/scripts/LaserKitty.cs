using UnityEngine;
using NewtonVR;

public class LaserKitty : MonoBehaviour
{
    NVRInteractableItem _nvr;
    Laser _laser;

    void Awake()
    {
        _nvr = GetComponent<NVRInteractableItem>();
        _laser = GetComponent<Laser>();
    }

    void Update()
    {
        if( _nvr.AttachedHand )
        {
            _laser.LaserOn = _nvr.AttachedHand.UseButtonPressed;
        }
    }
}
