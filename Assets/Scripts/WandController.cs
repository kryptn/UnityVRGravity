using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WandController : MonoBehaviour
{
    public Transform Field;
    public GameObject Dust;
    public GameObject SpawnPrefab;
    public int Force;
    private const EVRButtonId Trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    private const EVRButtonId Grip = EVRButtonId.k_EButton_Grip;
    private const EVRButtonId Pad = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private GameObject Spawn;


    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }
    private SteamVR_TrackedObject trackedObj;
    private GameObject Fields;

	// Use this for initialization
	void Start ()
	{
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
	    Fields = GetComponent<GameObject>();
        Spawn = Instantiate(SpawnPrefab);
	    Spawn.transform.parent = transform;
	}

    // Update is called once per frame
    void Update()
    {
        if (Controller == null)
            return;

        Spawn.GetComponent<Rigidbody>().mass = Controller.GetPress(Grip) ? 5000 : 0;

        //if (Controller.GetPress(Trigger))
        //    ObjectFactory.CreateProjectile(Dust, Field, Spawn.transform.position, transform.rotation, Force);

        if (Controller.GetPressDown(Pad))
        {
            var touch = Controller.GetAxis();
            var speed = Controller.GetAxis(Trigger);
            ObjectFactory.CreateSatellite(Dust, Spawn.transform.position, Spawn, touch.y+1, speed.x);
        }
    }
}
