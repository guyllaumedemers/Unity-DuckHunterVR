using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PrimaryButtonEvent : UnityEvent<bool> { }
public class SecondaryButtonEvent : UnityEvent<bool> { }

public class XRButtonWatcher : MonoBehaviour
{
    public XRBaseController xRBaseController;
    public InputDevice inputDevice;

    public PrimaryButtonEvent primaryButtonPressEvent;
    public SecondaryButtonEvent secondaryButtonPressEvent;
    private bool _primaryLastButtonState = false;
    private bool _secondaryLastButtonState = false;

    private void Awake()
    {
        xRBaseController = GetComponent<XRBaseController>();

        if (primaryButtonPressEvent == null)
            primaryButtonPressEvent = new PrimaryButtonEvent();

        if (secondaryButtonPressEvent == null)
            secondaryButtonPressEvent = new SecondaryButtonEvent();
    }

    private void Start()
    {
        if (xRBaseController.name.Contains("Left"))
        {
            if (XRInputManager.Instance.leftHandController != null)
                inputDevice = XRInputManager.Instance.leftHandController;

            if (inputDevice != null)
            {
                if (XRInputDebugger.Instance.inputDebugEnabled)
                    Debug.Log("ButtonWatcher has been linked to: " + inputDevice.name);
            }
            else
            {
                Debug.LogError("Unable to link ButtonWatcher to Left Controller.");
            }
        }

        if (xRBaseController.name.Contains("Right"))
        {
            if (XRInputManager.Instance.rightHandController != null)
                inputDevice = XRInputManager.Instance.rightHandController;

            if (inputDevice != null)
            {
                if (XRInputDebugger.Instance.inputDebugEnabled)
                    Debug.Log("ButtonWatcher has been linked to: " + inputDevice.name);
            }
            else
            {
                Debug.LogError("Unable to link ButtonWatcher to Right Controller.");
            }
        }
    }

    private void Update()
    {
        bool primaryTempState = false;
        bool primaryButtonState = false;

        bool secondaryTempState = false;
        bool secondaryButtonState = false;

        primaryTempState = inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState || primaryTempState;

        if (primaryTempState != _primaryLastButtonState)
        {
            primaryButtonPressEvent.Invoke(primaryTempState);
            _primaryLastButtonState = primaryTempState;
        }

        secondaryTempState = inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState) && secondaryButtonState || secondaryTempState;

        if (secondaryTempState != _secondaryLastButtonState)
        {
            secondaryButtonPressEvent.Invoke(secondaryTempState);
            _secondaryLastButtonState = secondaryTempState;
        }
    }
}
