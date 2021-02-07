using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class SecondaryButtonEvent : UnityEvent<bool> { }

public class ButtonWatcher : MonoBehaviour
{
    [field: SerializeField]
    public XRBaseController currentController;
    public InputDevice currentDevice;

    public PrimaryButtonEvent primaryButtonPress;
    public SecondaryButtonEvent secondaryButtonPress;

    private bool primaryLastButtonState = false;
    private bool secondaryLastButtonState = false;    

    private void Awake()
    {
        currentController = GetComponent<XRBaseController>();

        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        if (secondaryButtonPress == null)
        {
            secondaryButtonPress = new SecondaryButtonEvent();
        }
    }

    private void Start()
    {
        if (currentController.name.Contains("Right"))
        {
            currentDevice = GameController.Instance.rightHandedControllers[0];
            Debug.Log("Added ButtonWatcher to " + currentDevice.name);
        }

        if (currentController.name.Contains("Left"))
        {
            currentDevice = GameController.Instance.leftHandedControllers[0];
            Debug.Log("Added ButtonWatcher to " + currentDevice.name);
        }
    }

    private void Update()
    {
        bool primaryTempState = false;
        bool primaryButtonState = false;

        bool secondaryTempState = false;
        bool secondaryButtonState = false;

        primaryTempState = currentDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState || primaryTempState;

        if (primaryTempState != primaryLastButtonState)
        {
            primaryButtonPress.Invoke(primaryTempState);
            primaryLastButtonState = primaryTempState;
        }

        secondaryTempState = currentDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState) && secondaryButtonState || secondaryTempState;

        if (secondaryTempState != secondaryLastButtonState)
        {
            secondaryButtonPress.Invoke(secondaryTempState);
            secondaryLastButtonState = secondaryTempState;
        }

        Debug.Log(currentController.name + " Primary Button: " + primaryButtonState);
        Debug.Log(currentController.name + " Secondary Button: " + secondaryButtonState);
    }
}
