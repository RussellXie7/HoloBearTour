using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class FixPortal : MonoBehaviour, IInputClickHandler
{

    public GameObject portal1;
    public GameObject portal2;

    private bool isFixed;
    private Color myColor;

    // Use this for initialization
    void Start () {

        isFixed = false;
        myColor = transform.GetComponent<MeshRenderer>().material.color;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isFixed)
        {
            portal1.GetComponent<HandDraggable>().IsDraggingEnabled = false;
            portal2.GetComponent<HandDraggable>().IsDraggingEnabled = false;

            isFixed = true;

            transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else
        {
            portal1.GetComponent<HandDraggable>().IsDraggingEnabled = true;
            portal2.GetComponent<HandDraggable>().IsDraggingEnabled = true;

            isFixed = false;

            transform.GetComponent<MeshRenderer>().material.color = myColor;
        }
    }
}
