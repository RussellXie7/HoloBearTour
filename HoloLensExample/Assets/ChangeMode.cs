using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ChangeMode : MonoBehaviour
{
    public MeshRenderer x_ray;
    public MeshRenderer basic_info;
    public MeshRenderer artist;
    public MeshRenderer time_period;

    public GameObject x_ray_bear;
    public GameObject basic_bear;
    public GameObject time_bear;
    public GameObject artist_bear;

    private Color myColor;


    // Use this for initialization
    void Start () {

        myColor = basic_info.material.color;
        
    }
	
	
    public void ShowBear(PortalControl.PortalTypeEnum type)
    {
        switch (type)
        {
            case PortalControl.PortalTypeEnum.x_ray:
                x_ray.material.color = Color.cyan;
                basic_info.material.color = myColor;
                artist.material.color = myColor;
                time_period.material.color = myColor;

                basic_bear.SetActive(false);
                time_bear.SetActive(false);
                artist_bear.SetActive(false);
                x_ray_bear.SetActive(true);

                PortalPlayerControl.currType = type;

                break;
            case PortalControl.PortalTypeEnum.basic_info:
                basic_info.material.color = Color.cyan;
                artist.material.color = myColor;
                time_period.material.color = myColor;
                x_ray.material.color = myColor;

                x_ray_bear.SetActive(false);
                time_bear.SetActive(false);
                basic_bear.SetActive(true);
                artist_bear.SetActive(false);

                PortalPlayerControl.currType = type;
                break;
            case PortalControl.PortalTypeEnum.artist:
                artist.material.color = Color.cyan;
                time_period.material.color = myColor;
                x_ray.material.color = myColor;
                basic_info.material.color = myColor;

                basic_bear.SetActive(false);
                x_ray_bear.SetActive(false);
                artist_bear.SetActive(true);
                time_bear.SetActive(false);

                PortalPlayerControl.currType = type;
                break;
            case PortalControl.PortalTypeEnum.time_period:
                time_period.material.color = Color.cyan;
                basic_info.material.color = myColor;
                artist.material.color = myColor;
                x_ray.material.color = myColor;

                basic_bear.SetActive(false);
                x_ray_bear.SetActive(false);
                artist_bear.SetActive(false);
                time_bear.SetActive(true);

                PortalPlayerControl.currType = type;
                break;
        }
    }
}
