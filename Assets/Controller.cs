using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //public OVRInput.Controller controller = OVRInput.Controller.LTouch;
    public Transform controller;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.position;
        transform.rotation = controller.rotation;
        //transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        //transform.localRotation = OVRInput.GetLocalControllerRotation(controller);

    }
}