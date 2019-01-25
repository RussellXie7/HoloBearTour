using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour {

    public Transform bear;
    public float maxScale = 10;
    public float minScale = 0.1f;
    public float scaleSpeed = 1.0f;

    private bool rotateLeft_pressed = false;
    private bool rotateRight_pressed = false;
    private bool smaller_pressed = false;
    private bool bigger_pressed = false;

    private float m_startScale;
    private float m_scaleValue;

    private Vector3 m_savedPosition;
    private Quaternion m_savedRotation;
    private Vector3 m_savedScale;

	// Use this for initialization
	void Start () {
        m_startScale = bear.localScale.x;
        m_scaleValue = 1.0f;

        m_savedPosition = bear.position;
        m_savedRotation = bear.rotation;
        m_savedScale = bear.localScale;
    }

    public void ResetBear()
    {
        bear.position = m_savedPosition;
        bear.rotation = m_savedRotation;
        bear.localScale = m_savedScale;
        m_scaleValue = 1.0f;
    }

    public void OnRotateLeftDown() {
        rotateLeft_pressed = true;

        if (rotateRight_pressed)
        {
            rotateRight_pressed = false;
        }
    }

    public void OnRotateLeftUp()
    {
        rotateLeft_pressed = false;
    }

    public void OnRotateRightDown()
    {
        rotateRight_pressed = true;

        if (rotateLeft_pressed)
        {
            rotateLeft_pressed = false;
        }
    }

    public void OnRotateRightUp()
    {
        rotateRight_pressed = false;
    }

    public void OnSmallerDown()
    {
        smaller_pressed = true;

        if (bigger_pressed)
        {
            bigger_pressed = false;
        }
    }

    public void OnSmallerUp()
    {
        smaller_pressed = false;
    }

    public void OnBiggerDown()
    {
        bigger_pressed = true;

        if (smaller_pressed)
        {
            smaller_pressed = false;
        }
    }

    public void OnBiggerUp()
    {
        bigger_pressed = false;
    }

    // Update is called once per frame
    void Update () {

        if (bigger_pressed || smaller_pressed) {
            HandleScale(bigger_pressed, smaller_pressed);
        }

        if(rotateRight_pressed || rotateLeft_pressed)
        {
            HandleRotate(rotateLeft_pressed, rotateRight_pressed);
        }

	}

    private void HandleRotate(bool left, bool right)
    {
        if(!left && !right)
        {
            return;
        }

        if (left)
        {
            bear.RotateAround(bear.position, bear.up, Time.deltaTime * (90f));
        }

        if (right)
        {
            bear.RotateAround(bear.position, bear.up, Time.deltaTime * (-90f));
        }
    }

    private void HandleScale(bool l_bigger, bool l_smaller)
    {
        if(!l_bigger && !l_smaller)
        {
            return;
        }

        if (l_bigger)
        {
            m_scaleValue += Time.deltaTime;
        }

        if (l_smaller)
        {
            m_scaleValue -= Time.deltaTime;
        }

        if (m_scaleValue > maxScale)
        {
            m_scaleValue = maxScale;
        }

        if (m_scaleValue < minScale)
        {
            m_scaleValue = minScale;
        }

        float targetScale = m_startScale * m_scaleValue;

        bear.localScale = new Vector3(targetScale, targetScale, targetScale);

    }
}
