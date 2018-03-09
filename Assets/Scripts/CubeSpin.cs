using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpin : MonoBehaviour {
    public float shiftY = 0.2f;
    public float spinSpeed = 2f;

    private float maxY, minY;
    private bool increasing = true;
	// Use this for initialization
	void Start () {
        maxY = transform.localPosition.y + shiftY;
        minY = transform.localPosition.y - shiftY;
	}
	
	// Update is called once per frame
	void Update () {
        Spin();
        FloatAir();
	}

    private void Spin() {
        transform.Rotate(0, spinSpeed, 0, Space.Self);
    }

    private void FloatAir() {
        if(increasing)
        {
            transform.localPosition += new Vector3(0, Time.deltaTime * 0.2f, 0);
            
            if(transform.localPosition.y >= maxY)
            {
                increasing = false;
            }
        }
        else
        {
            transform.localPosition -= new Vector3(0, Time.deltaTime * 0.1f, 0);

            if(transform.localPosition.y <= minY)
            {
                increasing = true;
            }
        }
    }
}
