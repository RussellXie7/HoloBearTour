using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ChangeModeButton : MonoBehaviour, IInputClickHandler
{
    public PortalControl.PortalTypeEnum myButtonType;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        transform.parent.GetComponent<ChangeMode>().ShowBear(myButtonType);
    }
}
