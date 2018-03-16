using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class ColorShift : MonoBehaviour,IFocusable
{
    private float shiftOffset = 16.0f;
    private float USER_DISTANCE_VALUE = 3f;

    private Material myMat;
    private List<MeshRenderer> myMatAll;
    private List<SpriteRenderer> sp;
    private List<CanvasGroup> cg;

    private CanvasGroup closeInfo;

    private float shiftValue = 204f;
    private bool isIncreasing;
    private float user_distance;

	// Use this for initialization
	void Start () {
        
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            myMat = gameObject.GetComponent<MeshRenderer>().material;
        }
        else {
            myMat = null;
            myMatAll = new List<MeshRenderer>();
            foreach (MeshRenderer mesh in transform.GetComponentsInChildren<MeshRenderer>()) {
                myMatAll.Add(mesh);
            }

        }
        
        if (transform.childCount > 0)
        {
            sp = new List<SpriteRenderer>();
            cg = new List<CanvasGroup>();

            foreach (SpriteRenderer s in transform.GetComponentsInChildren<SpriteRenderer>()) {
                sp.Add(s);
            }

            foreach (CanvasGroup c in transform.GetComponentsInChildren<CanvasGroup>()) {
                if (c.gameObject.name != "CloseInfo")
                {
                    cg.Add(c);
                }
                else
                {
                    closeInfo = c;
                }
            }
        }
        else
        {
            sp = null;
            cg = null;
        }
	}
	
	// Update is called once per frame
	void Update () {

        // impossible value for checking
        user_distance = 999999;

        if (isIncreasing) {
            shiftValue -= shiftOffset;
            if (shiftValue <= 0) {
                shiftValue = 0;
            }
        }
        else
        {
            shiftValue += shiftOffset;
            if(shiftValue >= 204)
            {
                shiftValue = 204;
            }
        }
        if (myMat != null)
        {
            myMat.color = new Color(shiftValue / 255.0f, 204 / 255.0f, 204 / 255.0f);
        }
        else
        {
            foreach (MeshRenderer mesh in myMatAll)
            {
                mesh.material.color = new Color(shiftValue / 255.0f, 204 / 255.0f, 204 / 255.0f);
            }
        }

        if (sp != null)
        {
            SpriteRenderer curr_sp = null;

            // if we are using sprite on this body part
            if (sp.Count > 0)
            {
                curr_sp = sp[0];
                user_distance = Vector3.Distance(Camera.main.transform.position, curr_sp.transform.position);


                if (isIncreasing)
                {
                    if (user_distance != 999999 && user_distance < USER_DISTANCE_VALUE)
                    {
                        AdjustSPAlpha("close");
                    }
                    else if (user_distance != 999999)
                    {
                        AdjustSPAlpha("far");
                    }
                    else
                    {
                        Debug.Log("Error Happened, shoud not enter here in SP.");
                    }
                }
                else
                {
                    AdjustSPAlpha("decrease");
                }
            }
        }

        if (cg != null)
        {
            CanvasGroup curr_cg = null;

            // if we are usnig cg
            if (cg.Count > 0)
            {
                curr_cg = cg[0];
                user_distance = Vector3.Distance(Camera.main.transform.position, curr_cg.transform.GetChild(0).position);

                if (isIncreasing)
                {
                    if (user_distance != 999999 && user_distance < USER_DISTANCE_VALUE)
                    {
                        AdjustCGAlpha("close");
                    }
                    else if (user_distance != 999999)
                    {
                        AdjustCGAlpha("far");
                    }
                    else
                    {
                        Debug.Log("Error Happened, shoud not enter here. in CG");
                    }
                }
                else
                {
                    AdjustCGAlpha("decrease");
                }
            }
        }
	}


    private void AdjustSPAlpha(string command)
    {
        switch (command)
        {
            case "close":
                if (closeInfo != null && closeInfo.alpha < 1)
                {
                    closeInfo.alpha += shiftOffset / 204.0f;
                }

                foreach (SpriteRenderer sp in sp)
                {
                    if (sp.color.a > 0)
                    {
                        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, (sp.color.a - (shiftOffset / 204.0f)));
                    }
                }
                break;

            case "far":
                if (closeInfo != null && closeInfo.alpha > 0)
                {
                    closeInfo.alpha -= shiftOffset / 204.0f;
                }

                foreach (SpriteRenderer sp in sp)
                {
                    if (sp.color.a < 1)
                    {
                        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, (sp.color.a + (shiftOffset / 204.0f)));
                    }
                }
                break;

            case "decrease":
                if (closeInfo != null && closeInfo.alpha > 0)
                {
                    closeInfo.alpha -= shiftOffset / 204.0f;
                }

                foreach (SpriteRenderer sp in sp)
                {
                    if (sp.color.a > 0)
                    {
                        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, (sp.color.a - (shiftOffset / 204.0f)));
                    }
                }
                break;

            default:
                Debug.Log("wrong command given in sp");
                break;

        }
    }
    private void AdjustCGAlpha(string command)
    {
        switch (command)
        {
            case "close":

                if (closeInfo!=null && closeInfo.alpha < 1)
                {
                    closeInfo.alpha += shiftOffset / 204.0f;
                }

                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha > 0)
                    {
                        c.alpha -= shiftOffset / 204.0f;
                    }
                }
                break;
            case "far":
                if (closeInfo != null && closeInfo.alpha > 0)
                {
                    closeInfo.alpha -= shiftOffset / 204.0f;
                }

                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha < 1)
                    {
                        c.alpha += shiftOffset / 204.0f;
                    }
                }
                break;
            case "decrease":
                if (closeInfo != null && closeInfo.alpha > 0)
                {
                    closeInfo.alpha -= shiftOffset / 204.0f;
                }

                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha > 0)
                    {
                        c.alpha -= shiftOffset / 204.0f;
                    }
                }
                break;
            default:
                Debug.Log("wrong command given");
                break;

        }
    }

    public void OnFocusEnter()
    {
        if (isIncreasing) {
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
