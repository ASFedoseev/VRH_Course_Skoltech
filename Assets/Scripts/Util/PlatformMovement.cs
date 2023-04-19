using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
public class PlatformMovement : MonoBehaviour
{
    public float MoveSpeed;
    private float timer;
    Rigidbody rb;
    public Vector3  randomDir;
    [SerializeField] private GameObject[] platforms;
    private List<Rigidbody> platformRB;
    private Vector3[] offsets = {new Vector3(1, 0, 1), new Vector3(0, 0, 1), new Vector3(1, 0, 0)};
    [SerializeField] float distBetween = 1;
    // Start is called before the first frame update
    void Start()
    {
        platformRB = new List<Rigidbody>();
        foreach(GameObject platform in platforms)
        {
            platformRB.Add(platform.GetComponent<Rigidbody>());
        }
        randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        randomSpeed = Random.Range(0f, MoveSpeed);
        // randomDir = Vector3.forward;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public float randomSpeed;
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        // if (timer >= 2)
        // {
        //     randomDir = -new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        //     randomSpeed = Random.Range(-MoveSpeed, MoveSpeed);
        //     timer = 0;
        // }
        for(int i = 0; i < platformRB.Count; i++)
        {
            platformRB[i].MoveRotation(Quaternion.Euler(10f * Mathf.Sin(10f * Time.time + 3.14f), 0, 10f * Mathf.Sin(5f * Time.time)));
            platformRB[i].MovePosition(transform.position + distBetween * offsets[i] + randomDir * Time.fixedDeltaTime * randomSpeed);
        }
        // rb.MoveRotation(Quaternion.Euler(10f * Mathf.Sin(10f * Time.time + 3.14f), 0, 10f * Mathf.Sin(5f * Time.time)));
        rb.MovePosition(transform.position + randomDir * Time.fixedDeltaTime * randomSpeed);

    }
    public float lowerBound;
    public float upperBound;
    public void Reset()
    {
        randomSpeed = Random.Range(lowerBound, upperBound);
        randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            
    }

}
