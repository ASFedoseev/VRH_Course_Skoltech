using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPosition: MonoBehaviour
{
    public float velocity = 0.2f;
    public float angularVelocity = 20f;
    
    public float jumpforce = 3f;
    public float errmax = 0.5f;
    Rigidbody rigidBody = null;
    //public float hover_height = 2;
    // Start is called before the first frame update
    void Start()
    {
        //playAnim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * velocity;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * velocity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation *= Quaternion.Euler(0f, angularVelocity * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation *= Quaternion.Euler(0f, -angularVelocity * Time.deltaTime, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector3.up * jumpforce, ForceMode.VelocityChange);
        }
    }

    //void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit, hover_height))
    //    {
    //        float currHeight = hit.point.y + hover_height;
    //        float err = Mathf.Max(currHeight - transform.position.y, 0f);
    //        err = Mathf.Min(err, errmax);
    //        if (transform.position.y < currHeight)
    //        {
    //            rb.AddForce(Vector3.up * err, ForceMode.VelocityChange);
    //        }
    //    }
    //}
}
