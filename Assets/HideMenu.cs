using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class HideMenu : MonoBehaviour, IInputClickHandler
{
    private bool isOn = false;

	// Use this for initialization
	void Start () {
        isOn = false;
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        isOn = !isOn;

        if (isOn)
        {
            transform.GetComponentInChildren<Text>().text = "Hide";
            SendMessageUpwards("EnableAllChildren");
        }
        else
        {
            transform.GetComponentInChildren<Text>().text = "Show";
            SendMessageUpwards("DisableAllChildren");
            gameObject.SetActive(true);
        }

    }
}
