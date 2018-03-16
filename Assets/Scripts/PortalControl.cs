using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;


/// <summary>
/// Need to adapt the script to be relevant to user and delete this one for portal
/// </summary>
public class PortalControl : MonoBehaviour, IFocusable {

    public GameObject x_ray_bear;
    public GameObject basic_bear;
    public GameObject time_bear;
    public GameObject artist_bear;

    public AudioClip beep;
    private AudioSource myAudioSource;

    [HideInInspector]
    public static float USER_DISTANCE_OFFSET = 0.7f;

    private float shiftOffset = 16.0f;
    private List<CanvasGroup> cg;

    private float shiftValue = 204f;
    private bool isIncreasing;
    private float user_distance;

    private GameObject myCube;
    private bool isUserClose = false;

    public enum PortalTypeEnum {
        x_ray,
        time_period,
        basic_info,
        artist
    }

    public PortalTypeEnum PortalType = PortalTypeEnum.basic_info;
    // Use this for initialization
    void Start () {

        myCube = transform.Find("CubeSpin").gameObject;
        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.playOnAwake = false;
        myAudioSource.clip = beep;

        if (transform.childCount > 0)
        {
            cg = new List<CanvasGroup>();

            foreach (CanvasGroup c in transform.GetComponentsInChildren<CanvasGroup>())
            {
                cg.Add(c);
            }
        }
        else
        {
            cg = null;
        }
    }

    private void ShowPortalBear(PortalTypeEnum portal)
    {
        if(portal == PortalTypeEnum.x_ray && PortalPlayerControl.currType != portal) 
        {
            basic_bear.SetActive(false);
            time_bear.SetActive(false);
            artist_bear.SetActive(false);
            x_ray_bear.SetActive(true);

            PortalPlayerControl.currType = portal;
            myAudioSource.Play();
        }

        if(portal == PortalTypeEnum.time_period && PortalPlayerControl.currType != portal)
        {
            basic_bear.SetActive(false);
            x_ray_bear.SetActive(false);
            artist_bear.SetActive(false);
            time_bear.SetActive(true);

            PortalPlayerControl.currType = portal;
            myAudioSource.Play();
        }

        if (portal == PortalTypeEnum.artist && PortalPlayerControl.currType != portal)
        {
            basic_bear.SetActive(false);
            x_ray_bear.SetActive(false);
            artist_bear.SetActive(true);
            time_bear.SetActive(false);

            PortalPlayerControl.currType = portal;
            myAudioSource.Play();
        }

        if (portal == PortalTypeEnum.basic_info && PortalPlayerControl.currType != portal)
        {
            x_ray_bear.SetActive(false);
            time_bear.SetActive(false);
            basic_bear.SetActive(true);
            artist_bear.SetActive(false);

            PortalPlayerControl.currType = portal;
            myAudioSource.Play();
        }

        
    }

    private void HidePortalBear()
    {
        x_ray_bear.SetActive(false);
        time_bear.SetActive(false);
        basic_bear.SetActive(true);
    }
	
    private void UIHiding(float distance)
    {
        if(distance < USER_DISTANCE_OFFSET)
        {
            if (!isUserClose)
            {
                foreach (CanvasGroup c in cg)
                {
                    c.transform.gameObject.SetActive(false);
                    myCube.SetActive(false);
                }

                isUserClose = true;

                ShowPortalBear(PortalType);
            }

            
        }
        else
        {
            if (isUserClose)
            {
                foreach(CanvasGroup c in cg)
                {
                    c.transform.gameObject.SetActive(true);
                    myCube.SetActive(true);
                }

                isUserClose = false;

                // HidePortalBear();
            }

            
        }
    }

    private void UIFading(bool isOff)
    {
        if (isOff)
        {
            return;
        }

        if (cg != null && cg.Count > 0)
        {
            if (isIncreasing)
            {
                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha < 1)
                    {
                        c.alpha += shiftOffset / 204.0f;
                    }
                }
            }
            else
            {
                foreach (CanvasGroup c in cg)
                {
                    if (c.alpha > 0)
                    {
                        c.alpha -= shiftOffset / 204.0f;
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {

        user_distance = Vector3.Distance(myCube.transform.position, Camera.main.transform.position);

        UIHiding(user_distance);
        UIFading(isUserClose);
        
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
