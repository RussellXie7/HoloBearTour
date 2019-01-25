using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class FixPortal : MonoBehaviour, IInputClickHandler
{

    public List<GameObject> portals;
    
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
            foreach (GameObject portal in portals)
            {
                portal.GetComponent<HandDraggable>().IsDraggingEnabled = false;
            }

            isFixed = true;

            transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else
        {
            foreach (GameObject portal in portals)
            {
                portal.GetComponent<HandDraggable>().IsDraggingEnabled = true;
            }
            
            isFixed = false;

            transform.GetComponent<MeshRenderer>().material.color = myColor;
        }
    }
}
