using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class BearControl : MonoBehaviour,IInputClickHandler {

    private HandDraggable drag;
    private ExtendedManipulation scale;
	// Use this for initialization
	void Start () {
        drag = GameObject.Find("bear").GetComponent<HandDraggable>();
        scale = GameObject.Find("bear").GetComponent<ExtendedManipulation>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (drag.IsDraggingEnabled)
        {
            drag.SetDragging(false);
            scale.SetScaling(true);
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            drag.SetDragging(true);
            scale.SetScaling(false);
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
