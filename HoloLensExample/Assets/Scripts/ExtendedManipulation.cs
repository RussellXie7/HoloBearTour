using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using System;

/// <summary>
/// This script add extended manipulation for scaling up and down
/// Note that Rigidbody currently does not apply, and default target is this transform
/// </summary>
public class ExtendedManipulation : MonoBehaviour,IFocusable, IInputHandler, ISourceStateHandler {

    /// <summary>
    /// Event triggered when scaling starts
    /// </summary>
    public event Action StartedScaling;

    /// <summary>
    /// Event triggered when dragging stops
    /// </summary>
    public event Action StoppedScaling;


    [Tooltip("Scale by which hand movement in z is multiplied to move the dragged object.")]
    public float DistanceScale = 2f;


    [Tooltip("Maximum Scale You Want")]
    public float MaxScale = 100f;


    [Tooltip("Minimum Scale You Want")]
    public float MinScale = 0.1f;


    [Tooltip("Controls the speed at which the object will interpolate toward the desired scale")]
    [Range(0.01f, 1.0f)]
    public float ScaleLerpSpeed = 0.2f;

    public bool IsScalingEnabled = true;

    private bool isScaling;
    private bool isGazed;

    private Vector3 startPosition;
    private float startScale;
    

    private IInputSource currentInputSource;
    private uint currentInputSourceId;



	// Use this for initialization
	void Start () {

        // map the spectrum based on the scale of the object
        MaxScale = transform.localScale.x * MaxScale;
        MinScale = transform.localScale.x * MinScale;
	}

    private void OnDestroy()
    {
        if (isScaling) {
            StopScaling();
        }

        if (isGazed) {
            OnFocusExit();
        }
    }


    // Update is called once per frame
    void Update () {
        if (IsScalingEnabled && isScaling) {
            UpdateScaling();
        }
	}


    public void StartScaling() {
        if (!IsScalingEnabled) {
            return;
        }

        if (isScaling) {
            return;
        }

        InputManager.Instance.PushModalInputHandler(gameObject);

        isScaling = true;

        Vector3 inputPosition = Vector3.zero;

#if UNITY_2017_2_OR_NEWER
            InteractionSourceInfo sourceKind;
            currentInputSource.TryGetSourceKind(currentInputSourceId, out sourceKind);
            switch (sourceKind)
            {
                case InteractionSourceInfo.Hand:
                    currentInputSource.TryGetGripPosition(currentInputSourceId, out inputPosition);
                    break;
                case InteractionSourceInfo.Controller:
                    currentInputSource.TryGetPointerPosition(currentInputSourceId, out inputPosition);
                    break;
            }
#else
        currentInputSource.TryGetPointerPosition(currentInputSourceId, out inputPosition);
#endif

        startPosition = inputPosition;
        startScale = transform.localScale.x;

        StartedScaling.RaiseEvent();
        
    }

    public void SetScaling(bool isEnabled) {

        if (IsScalingEnabled == isEnabled) {

            return;
        }

        IsScalingEnabled = isEnabled;

        if (isScaling) {
            StopScaling();
        }
    }


    private void UpdateScaling() {
        Vector3 inputPosition = Vector3.zero;

#if UNITY_2017_2_OR_NEWER
            InteractionSourceInfo sourceKind;
            currentInputSource.TryGetSourceKind(currentInputSourceId, out sourceKind);
            switch (sourceKind)
            {
                case InteractionSourceInfo.Hand:
                    currentInputSource.TryGetGripPosition(currentInputSourceId, out inputPosition);
                    break;
                case InteractionSourceInfo.Controller:
                    currentInputSource.TryGetPointerPosition(currentInputSourceId, out inputPosition);
                    break;
            }
#else
        currentInputSource.TryGetPointerPosition(currentInputSourceId, out inputPosition);
#endif

        Vector3 newPosition = inputPosition;
        float h_diff = newPosition.y - startPosition.y;

        float scaleOffset = 10;
        float scaleFactor = 1;

        scaleFactor = ( h_diff * scaleOffset ) + scaleFactor;

        float targetScale = scaleFactor * startScale;

        if (targetScale > MaxScale) {
            targetScale = MaxScale;
        }

        if (targetScale < MinScale)
        {
            targetScale = MinScale;
        }

        float newScaleNum = Mathf.Lerp(transform.localScale.x, targetScale, ScaleLerpSpeed);

        transform.localScale = new Vector3(newScaleNum, newScaleNum, newScaleNum);

    }

    void StopScaling() {

        if (!isScaling)
        {
            return;
        }

        // Remove self as a model input handler
        InputManager.Instance.PopModalInputHandler();

        isScaling = false;
        currentInputSource = null;
        currentInputSourceId = 0;

        StoppedScaling.RaiseEvent();
    }


    public void OnFocusEnter() {
        if (!IsScalingEnabled)
        {
            return;
        }

        if (isGazed)
        {
            return;
        }

        isGazed = true;
    }

    public void OnFocusExit() {
        if (!IsScalingEnabled)
        {
            return;
        }

        if (!isGazed)
        {
            return;
        }

        isGazed = false;
    }

    public void OnInputDown(InputEventData eventData) {
        if (isScaling)
        {
            return;
        }

#if UNITY_2017_2_OR_NEWER
            InteractionSourceInfo sourceKind;
            eventData.InputSource.TryGetSourceKind(eventData.SourceId, out sourceKind);
            if (sourceKind != InteractionSourceInfo.Hand)
            {
                if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
                {
                    // The input source must provide positional data for this script to be usable
                    return;
                }
            }
#else
        if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
        {
            // The input source must provide positional data for this script to be usable
            return;
        }
#endif


        eventData.Use();

        currentInputSource = eventData.InputSource;
        currentInputSourceId = eventData.SourceId;

        FocusDetails? details = FocusManager.Instance.TryGetFocusDetails(eventData);

        // this gets the initial position of the object being looked at, not used though
        //Vector3 myStartPosition = (details == null)
        //    ? transform.position : details.Value.Point;

        StartScaling();

    }


    public void OnInputUp(InputEventData data) {

        if(currentInputSource != null &&
            data.SourceId == currentInputSourceId)
        {

            data.Use();

            StopScaling();
        }
    }
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        // Nothing to do
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
        {
            StopScaling();
        }
    }
}
