using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTransform : MonoBehaviour {
    public Rigidbody rigidBody;
    public  int currentScore = 0;
    public float forwardForse = 2000f;
    public float sidewayForse = 2000f;
    public void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(collision.collider.name);
        if (collision.gameObject.name == "Coin")
        {
            Destroy(collision.gameObject);
            currentScore++;
            Debug.Log(currentScore);
        }
    }
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        //rigidBody.useGravity = false;
        //Debug.Log(rigidBody.useGravity.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        //rigidBody.AddForce(0f,0f, forwardForse * Time.deltaTime);
        if (Input.GetKey("d"))
        {
            //Vector3.pose.
         
            rigidBody.AddForce(0f, 0f, sidewayForse * Time.deltaTime);

        }
        if (Input.GetKey("a"))
        {
           
            rigidBody.AddForce(0f, 0f, -sidewayForse * Time.deltaTime);

        }
        if (Input.GetKey("w"))
        {
           
            rigidBody.AddForce(-sidewayForse * Time.deltaTime, 0f, 0f);

        }
        if (Input.GetKey("s"))
        {
            
            rigidBody.AddForce(sidewayForse * Time.deltaTime, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.Space))
        {

            rigidBody.AddForce(0f, 20 * Time.deltaTime, 0f);

        }
    }
}
