using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
[System.Serializable]
public class XRInputManager : MonoBehaviour
{
    #region Singleton
    private static XRInputManager instance;

    private XRInputManager() { }

    public static XRInputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XRInputManager();
            }
            return instance;
        }
    }
    #endregion
    private List<InputDevice> _leftHandControllers;
    private List<InputDevice> _rightHandControllers;
    
    public InputDevice leftHandController;
    public InputDevice rightHandController;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RegisterControllers();
    }

    private void RegisterControllers()
    {
        _leftHandControllers = new List<InputDevice>();
        _rightHandControllers = new List<InputDevice>();

        //Left Hand Controller
        InputDeviceCharacteristics leftHandedCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(leftHandedCharacteristics, _leftHandControllers);

        if (_leftHandControllers.Count > 0)
        {
            leftHandController = _leftHandControllers[0];

            if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                Debug.Log(string.Format("Registered device name '{0}' ", leftHandController.name));
                XRInputDebugger.Instance.DebugLogLeftHand("Left device registered");
            }            
        }
        else
        {
            Debug.LogError("Unable to register Left Hand Controller. Please make sure that your VR Headset is turned on and the controllers are connected.");
            XRInputDebugger.Instance.DebugLogLeftHand("Unable to register");
        }

        //Right Hand Controller
        InputDeviceCharacteristics rightHandedCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightHandedCharacteristics, _rightHandControllers);

        if (_rightHandControllers.Count > 0)
        {
            rightHandController = _rightHandControllers[0];

            if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                Debug.Log(string.Format("Registered device name '{0}' ", rightHandController.name));
                XRInputDebugger.Instance.DebugLogRightHand("Right device registered");
            }            
        }
        else
        {
            Debug.LogError("Unable to register Right Hand Controller. Please make sure that your VR Headset is turned on and the controllers are connected.");
            XRInputDebugger.Instance.DebugLogRightHand("Unable to register");
        }
    }
}
