using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class XRay_Bear : MonoBehaviour, IFocusable {

    private List<MeshRenderer> renderers;
    private CanvasGroup cg;

    private bool isDecreasing;
    private float max = 1.0f;
    private float min = 0.35f;
    private float opacity = 1;
    
    private int bodyPartCount = 7;

    private float shiftOffset = 8.0f / 204.0f;
    private float canvasOffset;


	// Use this for initialization
	void Start () {

        renderers = new List<MeshRenderer>();
        cg = transform.Find("UIGroup").GetComponent<CanvasGroup>();
        canvasOffset = shiftOffset / (max - min);

        for(int i = 0; i < bodyPartCount; i++)
        {
            renderers.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (isDecreasing) {
            if (opacity > min)
            {
                opacity -= shiftOffset;

                foreach (MeshRenderer mr in renderers)
                {
                    mr.material.SetFloat("_Opacity", opacity);
                }
            }

            // canvas group should showup while bear fade out
            if(cg.alpha < 1)
            {
                cg.alpha += canvasOffset;
            }

        }
        else
        {
            if(opacity < max)
            {
                opacity += shiftOffset;

                foreach (MeshRenderer mr in renderers)
                {
                    mr.material.SetFloat("_Opacity", opacity);
                }
            }

            // canvas group should fade out
            if (cg.alpha > 0)
            {
                cg.alpha -= canvasOffset;
            }

        }


    }

    public void OnFocusEnter()
    {
        if (isDecreasing)
        {
            return;
        }

        isDecreasing = true;

    }

    public void OnFocusExit()
    {
        if (!isDecreasing)
        {
            return;
        }

        isDecreasing = false;
    }
}
