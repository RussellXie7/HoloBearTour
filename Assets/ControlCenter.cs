using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ControlCenter : MonoBehaviour, ISourceStateHandler {

    private List<GameObject> allChildren;

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    // Use this for initialization
    void Start () {

        allChildren = new List<GameObject>();

        foreach (Transform c in transform)
        {
            allChildren.Add(c.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DisableAllChildren()
    {
        foreach(GameObject g in allChildren)
        {
            g.SetActive(false);
        }
    }

    private void EnableAllChildren()
    {
        foreach (GameObject g in allChildren)
        {
            g.SetActive(true);
        }
    }

    public void OnSourceDetected(SourceStateEventData eventData) {
        EnableAllChildren();
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        DisableAllChildren();
    }
}
