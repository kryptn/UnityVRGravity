using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WandController : MonoBehaviour
{
    public GameObject Dust;
    public GameObject SpawnPrefab;
    public int Force;
    private const EVRButtonId Trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    private const EVRButtonId Grip = EVRButtonId.k_EButton_Grip;
    private const EVRButtonId Pad = EVRButtonId.k_EButton_SteamVR_Touchpad;
    public GameObject Spawn;

    public GameObject Other;
    public bool InControl;

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
	    Spawn.GetComponent<CursorController>().Initialize(Controller);
	    Spawn.transform.parent = transform;
	}

    // Update is called once per frame
    void Update()
    {
        if (Controller == null)
            return;

        /*
        var activated = Controller.GetAxis(Trigger).x > 0;
        if (activated && !Other.GetComponent<WandController>().InControl)
        {
            var other = Other.GetComponent<WandController>().Spawn;
            InControl = true;
            var midpoint = Vector3.Lerp(Spawn.transform.position, other.transform.position, 0.5f);


        }
        else
            InControl = false;
            */

        if (!Controller.GetPressDown(Pad)) return;

        var other = Other.GetComponent<WandController>().Spawn;
        var radius = Vector3.Distance(other.transform.position, Spawn.transform.position) * .9f;
        var midpoint = Vector3.Lerp(other.transform.position, Spawn.transform.position, 0.5f);
        var go = ObjectFactory.CreateSizeDefinedObject(Dust, midpoint, Quaternion.identity, radius);

        if (!Controller.GetPress(Trigger)) return;

        var force = Gravity.GetOrbitalVelocity(go);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    void Spawner()
    {
        
    }
}
