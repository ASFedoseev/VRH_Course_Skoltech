using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBySmoothDump : MonoBehaviour
{


    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 currentVelocity = Vector3.zero;
    public float targetVelocity = 0.0f;
    public Transform body;
    public string keyForward = "w";
    public string keyBack = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    // Start is called before the first frame update
    void Start()
        {
            //playAnim = GetComponent<Animator>();
            
        }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = Vector3.zero;
        if (Input.GetKey(keyRight))
        {
            //Vector3.pose.

            target.position += target.right * Time.deltaTime*targetVelocity;
            currentVelocity += target.right * Time.deltaTime * targetVelocity;


        }
        if (Input.GetKey(keyLeft))
        {

            target.position -= target.right * Time.deltaTime * targetVelocity;
            currentVelocity += target.right * Time.deltaTime * targetVelocity;

        }
        if (Input.GetKey(keyForward))
        {


            target.position += target.forward * Time.deltaTime * targetVelocity;
            currentVelocity += target.right * Time.deltaTime * targetVelocity;

        }
        if (Input.GetKey(keyBack))
        {

            target.position -= target.right * Time.deltaTime * targetVelocity;
            currentVelocity += target.right * Time.deltaTime * targetVelocity;

        }
        if (Input.GetKey(KeyCode.Space))
        {

            target.position += target.up * Time.deltaTime * targetVelocity;

        }
    }


    void FixedUpdate()
    {
        // Define a target position above and behind the target transform
 

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(body.position, target.position, ref currentVelocity, smoothTime);
    }
}
