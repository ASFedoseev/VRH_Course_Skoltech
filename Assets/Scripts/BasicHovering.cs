using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHovering : MonoBehaviour
{
    public Rigidbody dummy;
    public float thrustAccel = 9.81f;
    // Start is called before the first frame update
    void Start()
    {
        dummy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dummy.AddForceAtPosition(thrustAccel * dummy.mass * Vector3.up, dummy.centerOfMass);
    }
}
