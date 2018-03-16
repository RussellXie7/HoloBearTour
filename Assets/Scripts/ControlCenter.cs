using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ControlCenter : MonoBehaviour, ISourceStateHandler {

    public GameObject exemptedButton;

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
            if (c.gameObject != exemptedButton)
            {
                allChildren.Add(c.gameObject);
            }
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
            foreach(MeshRenderer mesh in g.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = false;
            }

            foreach(Collider cd in g.GetComponentsInChildren<Collider>())
            {
                cd.enabled = false;
            }

            foreach (Canvas c in g.GetComponentsInChildren<Canvas>())
            {
                c.enabled = false;
            }

        }
    }

    private void EnableAllChildren()
    {
        foreach (GameObject g in allChildren)
        {
            foreach (MeshRenderer mesh in g.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = true;
            }

            foreach (Collider cd in g.GetComponentsInChildren<Collider>())
            {
                cd.enabled = true;
            }

            foreach (Canvas c in g.GetComponentsInChildren<Canvas>())
            {
                c.enabled = true;
            }


        }
    }

    public void OnSourceDetected(SourceStateEventData eventData) {
        // still no idea the current condition
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        // no idea the correct condition
    }
}
