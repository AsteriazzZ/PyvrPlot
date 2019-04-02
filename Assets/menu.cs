using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {

    private Text txt;

	// Use this for initialization
	void Start () {
        txt = GameObject.Find("RightHand/Canvas/Text").GetComponent<Text>();

    }


    float time_passed = 0;
    // Update is called once per frame
    void Update () {
        time_passed += Time.deltaTime;

        if (time_passed > 10.0f)
        {
            txt.text = "";
        }

		if (Input.GetAxis("Oculus_GearVR_RIndexTrigger") > 0.9f)
        {
            txt.text = "(A) -- Enter movement mode. Lock onto a graph with the teal laser and move around with the controller.\n" + 
                       "(B) -- Enter scaling mode. Move controllers closer or further apart.\n" +
                       "(X) -- Enter rotate mode. Rotate left controller to rotate locked-on graph.";
            time_passed = 11.0f;
        } else if(time_passed > 10.0f)
        {
            txt.text = "";
        }
        
	}
}
