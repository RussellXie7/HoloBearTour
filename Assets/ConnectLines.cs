using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ConnectLines : MonoBehaviour, IInputClickHandler 
{
    private bool isConnectingLines;
    private Color myColor;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

    public Transform cube1;
    public Transform cube2;



    // Use this for initialization
    void Start () {
        isConnectingLines = false;
        myColor = transform.GetComponent<MeshRenderer>().material.color;

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 3;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }
	
	// Update is called once per frame
	void Update () {

        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if (isConnectingLines)
        {
            if(lineRenderer.enabled == false)
            {
                lineRenderer.enabled = true;
            }

            lineRenderer.SetPosition(0, (Camera.main.transform.position - new Vector3(0, 0.1f, 0)));
            
            if(Vector3.Distance(Camera.main.transform.position, cube1.position) > Vector3.Distance(Camera.main.transform.position, cube2.position))
            {
                lineRenderer.SetPosition(1, cube2.position);
                lineRenderer.SetPosition(2, cube1.position);
            }
            else
            {
                lineRenderer.SetPosition(1, cube1.position);
                lineRenderer.SetPosition(2, cube2.position);
            }
        }
        else
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        isConnectingLines = !isConnectingLines;

        if (isConnectingLines)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.color = myColor;
        }
        
    }
}
