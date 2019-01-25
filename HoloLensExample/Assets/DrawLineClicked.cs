using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;


public class DrawLineClicked : MonoBehaviour, IInputClickHandler {

    private bool isOn = false;
    private Color myColor;

    // Use this for initialization
    void Start () {
        isOn = false;
        myColor = transform.GetComponent<MeshRenderer>().material.color;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        isOn = !isOn;

        if (isOn)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
            GetComponent<DrawLineManager>().SetDrawing(true);
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.color = myColor;
            GetComponent<DrawLineManager>().SetDrawing(false);
        }

    }
}
