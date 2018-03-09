using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TimePeriodBear : MonoBehaviour, IFocusable
{

    private CanvasGroup cg;
    private bool isIncreasing;
    private float canvasOffset = 16.0f / 204.0f;

    // Use this for initialization
    void Start () {
        cg = transform.Find("UIGroup").GetComponent<CanvasGroup>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isIncreasing)
        {
            // canvas group should showup 
            if (cg.alpha < 1)
            {
                cg.alpha += canvasOffset;
            }

        }
        else
        {
            // canvas group should fade out
            if (cg.alpha > 0)
            {
                cg.alpha -= canvasOffset;
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
