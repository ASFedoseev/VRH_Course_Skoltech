using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByForce: MonoBehaviour 
{
    public Rigidbody rigidBody;
    public  int currentScore = 0;
    public float upwardForse = 2000f;
    public float sidewayForse = 2000f;
    public string keyForward = "w";
    public string keyBack = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

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

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(keyRight))
        {
            //Vector3.pose.

            rigidBody.AddForce(sidewayForse * Time.deltaTime, 0f, 0f, ForceMode.Impulse);

        }
        if (Input.GetKey(keyLeft))
        {

            rigidBody.AddForce(-sidewayForse * Time.deltaTime, 0f, 0f, ForceMode.Impulse);
        

        }
        if (Input.GetKey(keyForward))
        {
           
            
            rigidBody.AddForce(0f, 0f, sidewayForse * Time.deltaTime,ForceMode.Impulse);

        }
        if (Input.GetKey(keyBack))
        {

            Debug.Log("Moveforwarf!");
            rigidBody.AddForce(0f, 0f, -sidewayForse * Time.deltaTime,ForceMode.Acceleration);

        }
        if (Input.GetKey(KeyCode.Space))
        {

            rigidBody.AddForce(0f, upwardForse * Time.deltaTime, 0f, ForceMode.Impulse);

        }
        
    }

    void FixedUpdate()
    {
        //rigidBody.AddForce(0f, upwardForse * Time.deltaTime, 0f, ForceMode.Acceleration);
    }
}
