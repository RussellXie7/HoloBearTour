using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSense : MonoBehaviour {

    public GameObject debugCanvas;

    //private Text debugText;
    private GameObject player;


    // add big canvas and print size and distance.

	// Use this for initialization
	void Start () {
        player = Camera.main.gameObject;
        //debugText = debugCanvas.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float user_distance = Vector3.Distance(player.transform.position, transform.position);
        // float curr_size = transform.localScale.x;

        // string text = "Current Distance: " + user_distance + System.Environment.NewLine;
        // text += "Canvas Size: " + curr_size;

        // debugText.text = text;

        float new_size = 0.003f * user_distance + 0.001f;

        if(new_size > 0.0123f)
        {
            new_size = 0.0123f;
        }

        transform.localScale = new Vector3(new_size, new_size, new_size);
	}
}
