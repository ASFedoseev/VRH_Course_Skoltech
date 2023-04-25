using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDControlHover : MonoBehaviour
{
    public float thrust = 9.81f;
    public float raycastLength = 2.00f;
    public float K_p, K_d = 0.0f;
    private float lastError = 0.5f;
    private Rigidbody dummy;
    
    // Start is called before the first frame update
    void Start()
    {
        dummy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast (transform.position, -Vector3.up, out hit, raycastLength))
        {
            thrust = PDControl(hit.distance);
            dummy.AddForceAtPosition(thrust * Vector3.up, dummy.position);
        }
        
    }
    float PDControl(float rayLength)
    {
        float error = raycastLength - rayLength;
        float force = K_p * (error) + (K_d * (error - lastError )/Time.deltaTime);
        lastError = error;
        force = Mathf.Max(0f, force);
        return force;
    }
}
