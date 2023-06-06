using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float maxDistance = 20f;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int bounces = 1;
        lr.positionCount = 2;

        //Start and end point of the laser.
        Vector3 a = transform.position,
                b = transform.position + transform.forward * maxDistance;
        lr.SetPosition(0, transform.position);

        //Before we determine the endpoint, check if the laser is blocked
        while (bounces < lr.positionCount)
        {


            if (Physics.Linecast(a, b, out RaycastHit hit))
            {
                //Check if we have hit a mirror
                if (hit.transform.parent && hit.transform.parent.CompareTag("Mirror")) lr.positionCount++;
                b = hit.point;
                
            }
            lr.SetPosition(1, transform.position + transform.forward * maxDistance);
        }
    }
}
