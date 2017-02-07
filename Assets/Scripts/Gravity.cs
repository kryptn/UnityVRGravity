using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public static float GetOrbitalVelocity(GameObject body)
    {
        var G = body.GetComponent<ParticleController>().GravConstant;
        var largest =
            GameObject.FindGameObjectsWithTag("GravitationalBody")
                .Where(b => Vector3.Distance(body.transform.position, b.transform.position) < 2)
                .OrderBy(i => i.GetComponent<Rigidbody>().mass)
                .FirstOrDefault();
        if (largest == null) return 0;

        var mass = largest.GetComponent<Rigidbody>().mass + body.GetComponent<Rigidbody>().mass;
        var dist = Vector3.Distance(largest.transform.position, body.transform.position);

        return (Mathf.Sqrt(G * Mathf.Pow(mass, 2) / dist * mass)) * body.GetComponent<Rigidbody>().mass;
    }
}
