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

        drag.SetDragging(false);
        scale.SetScaling(false);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // disable -> drag mode with rotation
        if (!drag.IsDraggingEnabled && !scale.IsScalingEnabled)
        {
            drag.SetDragging(true);
            drag.RotationMode = HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright;
            scale.SetScaling(false);

            GetComponent<MeshRenderer>().material.color = Color.red;

        }
        // drag mode with rotation -> drag mode without rotation
        else if (drag.IsDraggingEnabled && drag.RotationMode == HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright)
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
        // scale mode -> disable
        else
        {
            drag.SetDragging(false);
            scale.SetScaling(false);

            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
