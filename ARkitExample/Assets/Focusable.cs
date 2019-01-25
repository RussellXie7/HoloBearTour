using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focusable : MonoBehaviour {

    private Material myMat;
    private List<MeshRenderer> myMatAll;
    private List<SpriteRenderer> sp;
    private List<CanvasGroup> cg;
    private Canvas textCanvas;

    private float shiftValue = 204f;
    private bool shiftComplete;
    private bool isIncreasing;

    // Use this for initialization
    void Start()
    {

        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            myMat = gameObject.GetComponent<MeshRenderer>().material;
        }
        else
        {
            myMat = null;
            myMatAll = new List<MeshRenderer>();
            foreach (MeshRenderer mesh in transform.GetComponentsInChildren<MeshRenderer>())
            {
                myMatAll.Add(mesh);
            }

        }

        if (transform.childCount > 0)
        {
            sp = new List<SpriteRenderer>();
            cg = new List<CanvasGroup>();

            foreach (SpriteRenderer s in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sp.Add(s);
            }
            foreach (CanvasGroup c in transform.GetComponentsInChildren<CanvasGroup>())
            {
                cg.Add(c);
            }

        }
        else
        {
            sp = null;
            cg = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isIncreasing)
        {
            shiftValue -= 4f;
            if (shiftValue <= 0)
            {
                shiftValue = 0;
            }
        }
        else
        {
            shiftValue += 4f;
            if (shiftValue >= 204)
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
            foreach (SpriteRenderer sp in sp)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, (204.0f - shiftValue) / 204.0f);
            }
        }

        if (cg != null)
        {
            foreach (CanvasGroup c in cg)
            {
                c.alpha = (204.0f - shiftValue) / 204.0f;
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
