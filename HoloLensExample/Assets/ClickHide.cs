using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.VR.WSA.Input;

public class ClickHide : MonoBehaviour, IInputClickHandler
{

    public bool TrippleClick = false;

    private float timeCounter = 0;
    private float tapCount = 0;

    private void Start()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("clicked");
    }
}
