using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using UnityEngine.VR.WSA.Input;

public class DrawLineManager : MonoBehaviour {

    public Material mat;

    public bool IsDrawingEnabled = false;
    private bool pressed = false;
    GameObject myText;

    private LineRenderer currLine;

    private int numClicks = 0;

    List<GameObject> lines;

    void Awake()
    {
        InteractionManager.SourcePressed += StartTracking;
        InteractionManager.SourceReleased += StopTracking;
        InteractionManager.SourceUpdated += OnTracking;
    }

    private void OnTracking(InteractionSourceState state) {

        if (pressed)
        {
            Vector3 pos;
            if (state.properties.location.TryGetPosition(out pos))
            {
                //myText.GetComponent<Text>().text = pos.ToString() + "\n" + numClicks + " and pc is " + currLine.positionCount;

                currLine.positionCount = numClicks + 1;
                currLine.SetPosition(numClicks, pos);
                numClicks++;

            }
        }

    }

    private void StopTracking(InteractionSourceState state) {
        pressed = false;
    }

    private void StartTracking(InteractionSourceState state)
    {
        if (!IsDrawingEnabled) {
            return;
        }
        pressed = true;

        GameObject go = new GameObject();

        lines.Add(go);
        currLine = go.AddComponent<LineRenderer>();
        currLine.startWidth = 0.02f;
        currLine.endWidth = 0.02f;
        currLine.material = mat;
        numClicks = 0;

        Vector3 pos;
        if (state.properties.location.TryGetPosition(out pos))
        {
            //myText.GetComponent<Text>().text = pos.ToString();
        }
    }



    public void SetDrawing(bool isOn) {

        // if it is turning off
        if (!isOn) {
            foreach (GameObject go in lines) {
                Destroy(go);
            }
            lines.Clear();
        }

        IsDrawingEnabled = isOn;
    }




    private void Start()
    {
        lines = new List<GameObject>();
        //myText = GameObject.Find("MixedRealityCameraParent").transform.GetChild(0).GetChild(2).GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update () {

    }
}
