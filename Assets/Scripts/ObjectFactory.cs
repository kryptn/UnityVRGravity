using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    

    public static GameObject CreateGameObject(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        return Instantiate(prefab, pos, rot);
    }




    public static GameObject CreateProjectile(GameObject source, Transform parent, Vector3 pos, Quaternion rot, int force)
    {
        var p = Instantiate(source, pos+parent.transform.forward*.2f, rot);
        p.GetComponent<ParticleController>().Initialize(parent);
        p.GetComponent<Rigidbody>().AddForce(p.transform.forward * force);
        p.GetComponent<SphereCollider>().enabled = true;
        return p;
    }

    public static GameObject CreateTrail(GameObject source, Vector3 pos)
    {
        var trail = Instantiate(source, pos, Quaternion.identity);
        
        trail.GetComponent<Rigidbody>().useGravity = false;
        return trail;
    }

    public static GameObject CreateSatellite(GameObject source, Vector3 pos, GameObject parent, float scale, float speed)
    {
        // 
        var mass = scale * 50;
        var radius = 0.001f * mass;

        var satellite = Instantiate(source, pos, parent.transform.rotation);
        satellite.transform.localScale = new Vector3(radius, radius, radius);
        satellite.tag = "GravitationalBody";
        satellite.GetComponent<ParticleController>().Initialize(parent);
        satellite.GetComponent<SphereCollider>().enabled = true;
        satellite.GetComponent<Rigidbody>().useGravity = false;
        satellite.GetComponent<Rigidbody>().mass = mass;
        satellite.GetComponent<Rigidbody>().AddForce(parent.transform.forward * speed*100*mass);
        
        return satellite;
    }
}
