using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ParticleController : MonoBehaviour
{
    public Material Dark;
    public Material Cool;
    public Material Warm;
    public Material Hot;


    public int Lifetime;
    public float GravConstant;
    private GameObject AttractedTo;
    private DateTime deathTime;

    private Rigidbody self;

    public ComputeShader GravityShader;

	// Use this for initialization
	void Start ()
	{
	    deathTime = DateTime.Now + TimeSpan.FromSeconds(Lifetime);
	    self = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        if (AttractedTo == null) return;
        
	    var Bodies = GameObject.FindGameObjectsWithTag("GravitationalBody").Where(body => body != gameObject);
	    var close = 0;
	    var forces = Bodies.Select(body =>
	    {
            var dist = Vector3.Distance(transform.position, body.transform.position);
	        if (dist < 2)
	            close += 1;
	        var dir = body.transform.position - transform.position;
            var force = (GravConstant * self.mass * body.GetComponent<Rigidbody>().mass) / (dist * dist);
            return force * dir * Time.fixedDeltaTime;
	    });
	    self.AddForce(forces.Aggregate(new Vector3(), (current, f) => current + f));


	    if (close <= 2)
            GetComponent<Renderer>().material = Dark;
        else if(close <= 6)
            GetComponent<Renderer>().material = Cool;
        else if (close <= 12)
            GetComponent<Renderer>().material = Warm;
        else
            GetComponent<Renderer>().material = Hot;


    }

    void Update()
    {

        if (Lifetime > 0 && DateTime.Now > deathTime)
            Destroy(gameObject);

        if (AttractedTo == null) return;

        var dist = Vector3.Distance(transform.position, AttractedTo.transform.position);
        if (dist > 500)
            Destroy(gameObject);
    }

    public void Initialize(Transform parent)
    {
        transform.parent = parent;
    }

    public void Initialize(Transform parent, GameObject attractedTo)
    {
        transform.parent = parent;
        AttractedTo = attractedTo;
    }

    public void Initialize(GameObject attractedTo)
    {
        AttractedTo = attractedTo;
    }
}
