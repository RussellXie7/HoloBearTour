using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class PortalControl : MonoBehaviour, IFocusable {
    private float shiftOffset = 16.0f;
    private List<CanvasGroup> cg;

    private float shiftValue = 204f;
    private bool isIncreasing;
    private float user_distance;

    // Use this for initialization
    void Start () {
        if (transform.childCount > 0)
        {
            cg = new List<CanvasGroup>();

            foreach (CanvasGroup c in transform.GetComponentsInChildren<CanvasGroup>())
            {
                cg.Add(c);
            }
        }
        else
        {
            cg = null;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (cg != null && cg.Count > 0)
        {
            if (isIncreasing)
            {
                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha < 1)
                    {
                        c.alpha += shiftOffset / 204.0f;
                    }
                }
            }
            else
            {
                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha > 0)
                    {
                        c.alpha -= shiftOffset / 204.0f;
                    }
                }
            }
        }

    }

    public void OnFocusEnter()
    {
        if (isIncreasing)
        {
            return;
        }

        isIncreasing = true;

    }

    public void OnFocusExit()
    {
        if (!isIncreasing)
        {
            return;
        }

        isIncreasing = false;
    }
}
