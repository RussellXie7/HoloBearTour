using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlayerControl : MonoBehaviour {

    // This script cares about switching rendered bear.

    public static PortalControl.PortalTypeEnum currType;

	// Use this for initialization
	void Start () {
        currType = PortalControl.PortalTypeEnum.basic_info;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
