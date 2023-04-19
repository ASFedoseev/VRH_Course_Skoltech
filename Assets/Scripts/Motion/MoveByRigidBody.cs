using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveByRigidBody: MonoBehaviour 
{
    public Rigidbody rigidBody;
    public float upwardForse = 2000f;
    public float sidewayForse = 2000f;
    public string keyForward = "w";
    public string keyBack = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    //public InputField velocityF = null;

    //public void OnCollisionEnter(Collision collision)
    //{
    //   // Debug.Log(collision.collider.name);
    //    if (collision.gameObject.name == "Coin")
    //    {
    //        Destroy(collision.gameObject);
    //        currentScore++;
    //        Debug.Log(currentScore);
    //    }
    //}



    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        //upwardForse = float.Parse(velocityF.text);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(keyRight))
        {
            //Vector3.pose.

            rigidBody.MoveRotation(rigidBody.transform.rotation * new Quaternion(0f, sidewayForse * Time.deltaTime, 0f, 1f).normalized);


        }
        if (Input.GetKey(keyLeft))
        {

            //rigidBody.MovePosition(rigidBody.transform.position + new Vector3(-sidewayForse * Time.deltaTime, 0f, 0f));
            rigidBody.MoveRotation(rigidBody.transform.rotation * new Quaternion(0f, -sidewayForse * Time.deltaTime, 0f, 1f).normalized);

        }
        if (Input.GetKey(keyForward))
        {
            
            //rigidBody.MovePosition(rigidBody.transform.position + new Vector3(0f, 0f, sidewayForse * Time.deltaTime));
            rigidBody.MovePosition(rigidBody.transform.position + rigidBody.transform.forward * sidewayForse * Time.deltaTime);
        }
        if (Input.GetKey(keyBack))
        {

            //rigidBody.MovePosition(rigidBody.transform.position + new Vector3(0f, 0f, -sidewayForse * Time.deltaTime));
            rigidBody.MovePosition(rigidBody.transform.position - rigidBody.transform.forward * sidewayForse * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.Z))
        {

            rigidBody.MovePosition(rigidBody.transform.position + new Vector3(0f, -upwardForse * Time.deltaTime, 0f));

        }
        if (Input.GetKey(KeyCode.X))
        {

            rigidBody.MovePosition(rigidBody.transform.position + new Vector3(0f, upwardForse * Time.deltaTime, 0f));

        }

    }

    void FixedUpdate()
    {
       // rigidBody.AddForce(0f, upwardForse * Time.deltaTime, 0f, ForceMode.Acceleration);
    }
}
