using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInteraction : MonoBehaviour {

    private GameObject tempObj = null;
    private GameObject currObj = null;
    private GameObject prevObj = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.forward,out hit, 1000f))
        {
            tempObj = hit.transform.gameObject;

            if(currObj != tempObj)
            {
                prevObj = currObj;
                currObj = tempObj;

                //Debug.Log("current object: " + currObj.name);
                //Debug.Log("previous object: " + prevObj.name);

                if (currObj && currObj.GetComponent<Focusable>()) {
                    currObj.GetComponent<Focusable>().OnFocusEnter();
                }
                else if(currObj && currObj.transform.parent && currObj.transform.parent.GetComponent<Focusable>()){
                    currObj.transform.parent.GetComponent<Focusable>().OnFocusEnter();
                }

                if (prevObj && prevObj.GetComponent<Focusable>())
                {
                    prevObj.GetComponent<Focusable>().OnFocusExit();
                }
                else if(prevObj && prevObj.transform.parent && prevObj.transform.parent.GetComponent<Focusable>()){
                    prevObj.transform.parent.GetComponent<Focusable>().OnFocusExit();
                }
            }

        }
	}
}
