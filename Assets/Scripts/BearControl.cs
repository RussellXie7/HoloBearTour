using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class BearControl : MonoBehaviour,IInputClickHandler
{
    public string dragObjectName = "bear";
    public string scaleObjectName = "bear";

    private HandDraggable drag;
    private ExtendedManipulation scale;
    
	// Use this for initialization
	void Start () {
        drag = GameObject.Find(dragObjectName).GetComponent<HandDraggable>();
        scale = GameObject.Find(scaleObjectName).GetComponent<ExtendedManipulation>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // drag mode with rotation -> drag mode without rotation
        if (drag.IsDraggingEnabled && drag.RotationMode == HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright)
        {
            drag.RotationMode = HandDraggable.RotationModeEnum.LockObjectRotation;

            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        // drag mode without rotation -> scale mode
        else if (drag.IsDraggingEnabled && drag.RotationMode == HandDraggable.RotationModeEnum.LockObjectRotation)
        {
            drag.SetDragging(false);
            scale.SetScaling(true);

            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        // scale mode -> drag mode with rotation
        else
        {
            drag.SetDragging(true);
            drag.RotationMode = HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright;
            scale.SetScaling(false);

            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
