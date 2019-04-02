using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaling : MonoBehaviour {

    public GameObject controller1;
    public GameObject controller2;

    public static GameObject target;

    private float hemisphere_radius = 10;

    private bool isMoveMode = false;
    private bool isScaleMode = false;
    private bool isRotateMode = false;
    
    float startContrDistance = 0;
    Vector3 initialScale = Vector3.zero;

    Quaternion initialTouchRotation = Quaternion.identity;
    Quaternion initialGraphRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("targetStart");
	}
	
	// Update is called once per frame
	void Update () {

        print(target);
        print(calcRayHemisphereIntersection(Vector3.zero, new Vector3(0,-1,0), Vector3.zero));
            
        //turn on move mode
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (isMoveMode)
            {
                isMoveMode = false;
                target = GameObject.Find("targetStart");
            }
            else
            {
                isMoveMode = true;
            }
            
        }
        

        //turn on scale mode
        if (OVRInput.GetDown(OVRInput.Button.Two)) {
            isScaleMode = !isScaleMode;
            startContrDistance = getControllerDistance();
            initialScale = target.transform.localScale;
        }

        //turn on rotate mode
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            isRotateMode = !isRotateMode;
            initialTouchRotation = controller1.transform.rotation;
            initialGraphRotation = target.transform.rotation;
        }


        if (isMoveMode)
        {
            Vector3 new_target_pos = calcRayHemisphereIntersection(Vector3.zero, controller2.transform.forward, controller2.transform.position);
            if (new_target_pos != Vector3.zero) {
                Vector3 diff = (target.transform.localScale[0]) * (target.transform.rotation * new Vector3(0.5f, 0.5f, 0.5f));
                target.transform.position = new_target_pos - diff;
                print(target.transform.position);
                //print("distance: " + Vector3.Distance(new_target_pos, controller2.transform.position));
            }
        }
        
        if (isScaleMode && getControllerDistance() != startContrDistance)
        {
            float contrDist = getControllerDistance();
            float rateDist = contrDist/startContrDistance;
            target.transform.localScale = initialScale * rateDist;
        }

        if (isRotateMode)
        {
            Quaternion diffTouchRotate = Quaternion.Inverse(initialTouchRotation) * controller1.transform.rotation;
            target.transform.rotation = diffTouchRotate * initialGraphRotation;
        }
        /*
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            RaycastHit hit;

            Debug.DrawRay(controller2.transform.position, controller2.transform.forward * 1000, Color.red, 20);
            if (Physics.Raycast(controller2.transform.position, controller2.transform.forward, out hit))
            {
                target = hit.transform.gameObject;
            }
        }
        */
    
        if ( OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) != Vector2.zero)
        {
            Vector2 movement = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            target.transform.position += new Vector3(movement[0]*0.1f, 0, movement[1]*0.1f);
        }
	}

    float getControllerDistance()
    {
        Vector3 rpos = controller2.transform.position;
        Vector3 lpos = controller1.transform.position;
        return Vector3.Distance(rpos, lpos);
    }

    /*
     * c - center point of sphere
     * r - radius of sphere
     * l - direction of line
     * o - origin of line
     * 
     * If return (0,0,0) means line does not intersect with hemisphere
     */

    public GameObject mySphere;
    Vector3 calcRayHemisphereIntersection(Vector3 c, Vector3 l, Vector3 o)
    {
        float r = hemisphere_radius;
        l = l.normalized;

        float test = Mathf.Pow(Vector3.Dot(l,(o - c)),2) - Mathf.Pow((o-c).magnitude,2) + Mathf.Pow(r,2);

        print(test);
        if (test < 0)
            return Vector3.zero;

        float d = -Vector3.Dot(l, (o - c)) + Mathf.Sqrt(test);
        print("d :" + d);

        Vector3 solution = o + (l * d);

        if(solution[1] < 0)
            return Vector3.zero;

        return solution;

    }
}
