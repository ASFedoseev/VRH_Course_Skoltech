using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObjects : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    bool isOccupaed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Drone")
        {
            isOccupaed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Drone")
        {
            isOccupaed = false;
        }
    }

    public bool getOccupation()
    {
        return isOccupaed;
    }

}
