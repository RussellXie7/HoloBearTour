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

    public List<Transform> cubes;

    public Material material;
    public Transform anchor;

    private float USER_DISTANCE_OFFSET = PortalControl.USER_DISTANCE_OFFSET;

    // Use this for initialization
    void Start () {
        isConnectingLines = false;
        myColor = transform.GetComponent<MeshRenderer>().material.color;

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.positionCount = 5;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }

	private int FindClosestCube(List<float> userToCube)
    {
        int closestCube = -1;
        float closestDis = 9999999;

        foreach (Transform cube in cubes)
        {
            userToCube.Add(Vector3.Distance(Camera.main.transform.position, cube.position));
        }

        for(int i = 0; i < userToCube.Count; i++)
        {
            if(closestDis > userToCube[i])
            {
                closestDis = userToCube[i];
                closestCube = i;
            }
        }

        return closestCube;

    }
    // Update is called once per frame
    void Update () {

        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if (isConnectingLines)
        {
            List<float> userToCube = new List<float>();

            int curr_index = FindClosestCube(userToCube);

            lineRenderer.enabled = true;

            if (userToCube[curr_index] < USER_DISTANCE_OFFSET)
            {
                // I want to hide the line from user when user is in range
                lineRenderer.SetPosition(0, cubes[curr_index].position);
            }
            else
            {
                lineRenderer.SetPosition(0, (anchor.position));
            }

            lineRenderer.SetPosition(1, cubes[curr_index].position);
            curr_index = GetNextIndex(curr_index);
            lineRenderer.SetPosition(2, cubes[curr_index].position);
            curr_index = GetNextIndex(curr_index);
            lineRenderer.SetPosition(3, cubes[curr_index].position);
            curr_index = GetNextIndex(curr_index);
            lineRenderer.SetPosition(4, cubes[curr_index].position);


        }
        else
        {
            lineRenderer.enabled = false;
        }
	}

    private int GetNextIndex(int curr)
    {
        if (curr + 1 >= cubes.Count)
        {
            return 0;
        }
        else
        {
            return (curr + 1);
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
