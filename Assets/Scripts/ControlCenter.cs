using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ControlCenter : MonoBehaviour, ISourceStateHandler {

    private List<GameObject> allChildren;

#if UNITY_EDITOR
    void Awake()
    {
        Application.targetFrameRate = 30;
    }
#endif

    // Use this for initialization
    void Start () {

        allChildren = new List<GameObject>();

        foreach (Transform c in transform)
        {
            allChildren.Add(c.gameObject);
        }

        // DisableAllChildren();
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
        // still no idea the current condition
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        // no idea the correct condition
    }
}
