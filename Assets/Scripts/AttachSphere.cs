using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachSphere : MonoBehaviour {
    
	// Update is called once per frame
	void Update ()
	{
	    transform.position = transform.parent.position + transform.parent.forward*0.15f;
	}
}
