using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePointLidar : MonoBehaviour
{
    public Transform rayTarget = null;
    public LayerMask Layers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray newRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 10f,
            Color.magenta);

        RaycastHit rayCollision;
        if (Physics.Raycast(newRay, out rayCollision,Mathf.Infinity,Layers))
        {
            rayTarget.gameObject.SetActive(true);
            rayTarget.position = rayCollision.point;
            Debug.Log(rayCollision.distance.ToString());
        }
        else

        {
            rayTarget.gameObject.SetActive(false);
        }
    }
}
