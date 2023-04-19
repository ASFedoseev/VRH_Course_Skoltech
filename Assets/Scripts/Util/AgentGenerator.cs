using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGenerator : MonoBehaviour
{

    public GameObject agent;
    public int numOfAgents;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numOfAgents; i++)
        {
            GameObject cartPole = Instantiate(agent, new Vector3(-i, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
