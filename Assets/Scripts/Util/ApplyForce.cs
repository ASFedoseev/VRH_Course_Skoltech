using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] Transform parentDrone;
    [SerializeField] float forceMagnitude;
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Drone")
        {
            Vector3 forceDir = (other.gameObject.transform.position - parentDrone.transform.position);
            other.gameObject.GetComponent<Rigidbody>().AddForce(forceMagnitude*forceDir.normalized / Mathf.Max(forceDir.magnitude, 1f));
            // other.gameObject.GetComponent<SwarmDynamicLandingAgent>().AddReward(-10);
            Debug.DrawRay(other.gameObject.transform.position, 10 * forceMagnitude*forceDir, Color.blue);
        }
    }
    }
