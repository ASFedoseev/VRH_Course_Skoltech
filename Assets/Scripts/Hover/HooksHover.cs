using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HooksHover : MonoBehaviour
{
    public Rigidbody dummy;
    public float thrust = 9.81f;
    public float raycastLength = 2.11f;
    private float lastHitDist = 0.0f;
    public float K_p, K_d = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        dummy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, raycastLength))
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastLength)) 
        {
            float force = HooksLaw(hit.distance);
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            dummy.AddForceAtPosition(Vector3.up*force, dummy.position);
        }
        else
        {
            lastHitDist = raycastLength * 1.1f;
        }


    }
    float HooksLaw(float hitRayLength)
    {
        float force = K_p * (raycastLength - hitRayLength) + (K_d * (lastHitDist - hitRayLength) /Time.deltaTime);
        force = Mathf.Max(0f, force);
       
        lastHitDist = hitRayLength;
        return force;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, -Vector3.up* raycastLength);
 



        //Gizmos.DrawSphere(hapticTargetPosition, 0.01f);
        Gizmos.DrawSphere(transform.position, 0.01f);





    }
}
