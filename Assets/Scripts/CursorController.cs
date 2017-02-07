using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CursorController : MonoBehaviour
{


    private SteamVR_Controller.Device Controller;
    private const EVRButtonId Trigger = EVRButtonId.k_EButton_SteamVR_Trigger;

    private bool PickedUp;

    // Update is called once per frame
    void Update ()
	{
	    transform.position = transform.parent.position + transform.parent.forward*0.15f;
	}

    void OnCollisionStay(Collision collision)
    {
        if (Controller.GetPress(Trigger))
        {
            PickedUp = true;
            collision.collider.transform.parent = transform;
        }
        else
        {
            if (PickedUp)
            {
                PickedUp = false;
                collision.collider.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            }
            collision.collider.transform.parent = null;
        }
    }

public void Initialize(SteamVR_Controller.Device controller)
    {
        Controller = controller;
    }

}
