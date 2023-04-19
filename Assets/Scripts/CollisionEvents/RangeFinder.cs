using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFinder : MonoBehaviour
{
    public Transform rayTarget = null;
    public LayerMask Layers;
    public GameObject lastTarget = null;
    public Color lastColor;
    private void Start()
    {
        lastTarget = this.gameObject;
        lastColor = this.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame

    void Update()
    {
        Ray newRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);
        float r = 4.0f;
        for (var i = -45; i <= 45; i += 5)
        {
            float x = (r * Mathf.Cos(i * Mathf.PI / 180));
            float y = (r * Mathf.Sin(i * Mathf.PI / 180));
            RaycastHit rayCollision;
            var rayDirection = transform.TransformPoint(new Vector3(x, 0, y));


            if (Physics.Raycast(newRay, out rayCollision, Mathf.Infinity, Layers))
            {
                rayTarget.gameObject.SetActive(true);
                if ((lastTarget.name != rayCollision.collider.gameObject.name) && (rayCollision.collider.gameObject != null))
                {

                    lastTarget.GetComponent<Renderer>().material.color = lastColor;
                    lastColor = rayCollision.collider.gameObject.GetComponent<Renderer>().material.color;

                    lastTarget = rayCollision.collider.gameObject;

                    rayTarget.position = rayCollision.point;
                    rayCollision.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }

                GetComponent<Renderer>().material.color = Color.yellow;

            }
            else
            {
                GetComponent<Renderer>().material.color = Color.gray;
                rayTarget.gameObject.SetActive(false);

            }
            Debug.DrawLine(transform.position, rayDirection, Color.cyan);
        }

    }
}
