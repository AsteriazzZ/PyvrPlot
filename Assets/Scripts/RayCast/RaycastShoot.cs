using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    public float rayRange = 50f;
    public GameObject controller;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private LineRenderer laserLine;
    private GameObject transformer;
  

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (!laserLine.enabled)
            {
                laserLine.enabled = true;
            }
            else
            {
                laserLine.enabled = false;
            }

        }


        if (laserLine.enabled)
        {
            print(controller.transform.position);

            RaycastHit hit;
            //laserLine.SetPosition(0, controller.transform.position);
            laserLine.SetPosition(0, Vector3.zero);

            float dist = Vector3.Distance(controller.transform.position, calcRayHemisphereIntersection(Vector3.zero, controller.transform.forward, controller.transform.position));
           
            laserLine.SetPosition(1, new Vector3(0, 0, dist*100));
            if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit, rayRange))
            {
                //print("Hit!!!");
                //out.distance
                //laserLine.SetPosition(1, hit.point);
                //laserLine.SetPosition(1, new Vector3(0, 0, hit.distance));
                //print("hit point is"+hit.point);
                scaling.target = hit.transform.gameObject;
            }
            else
            {
                //laserLine.SetPosition(1, controller.transform.forward * rayRange);
                //laserLine.SetPosition(1, controller.transform.localRotation.eulerAngles * rayRange);
                //laserLine.SetPosition(1, new Vector3(0,0,rayRange));
            }

            //print(controller.transform.forward);
            
            //laserLine.SetPosition(0, controller.transform.position);
            //laserLine.SetPosition(1, calcRayHemisphereIntersection(Vector3.zero, controller.transform.forward, controller.transform.position));
            
        }
    }


    private void confirmChoice()
    {
        
    }

    float hemisphere_radius = 10;
    Vector3 calcRayHemisphereIntersection(Vector3 c, Vector3 l, Vector3 o)
    {
        float r = hemisphere_radius;
        l = l.normalized;

        float test = Mathf.Pow(Vector3.Dot(l, (o - c)), 2) - Mathf.Pow((o - c).magnitude, 2) + Mathf.Pow(r, 2);

        print(test);
        if (test < 0)
            return Vector3.zero;

        float d = -Vector3.Dot(l, (o - c)) + Mathf.Sqrt(test);
        print("d :" + d);

        Vector3 solution = o + (l * d);

        if (solution[1] < 0)
            return Vector3.zero;

        return solution;

    }

}
