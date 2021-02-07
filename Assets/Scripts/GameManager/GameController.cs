using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
[System.Serializable]
public class GameController : MonoBehaviour
{
    #region Singleton
    private static GameController instance;

    private GameController() { }

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }
    #endregion

    public List<InputDevice> RightHandControllers { get; set; }
    public List<InputDevice> LeftHandControllers { get; set; }
    
    public CCCanvas m_inGameDebugCanvas;
    public CCCanvas m_rightHandDebugCanvas;
    public CCCanvas m_leftHandDebugCanvas;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RegisterControllers();
        DebugLogInGame("Debug line 1\n" + "Debug line 2");
    }

    // Update is called once per frame
    void Update()
    {
        //CheckLeftHandedInputs();
        //CheckRightHandedInputs();
    }

    void RegisterControllers()
    {
        RightHandControllers = new List<InputDevice>();
        LeftHandControllers = new List<InputDevice>();

        //Right Hand Controller
        InputDeviceCharacteristics rightHandedCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightHandedCharacteristics, RightHandControllers);

        if (RightHandControllers.Count > 0)
        {
            foreach (InputDevice device in RightHandControllers)
            {
                Debug.Log(string.Format("Registered device name '{0}' ", device.name));
                DebugLogRightHand("Device registered");
            }
        }
        else
        {
            Debug.LogError("Unable to register Right Hand Controller. Please make sure that your VR Headset is turned on and the controllers are connected.");
            DebugLogRightHand("Unable to register");
        }

        //Left Hand Controller
        InputDeviceCharacteristics leftHandedCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(leftHandedCharacteristics, LeftHandControllers);

        if (LeftHandControllers.Count > 0)
        {
            foreach (InputDevice device in LeftHandControllers)
            {
                Debug.Log(string.Format("Registered device name '{0}' ", device.name));
                DebugLogLeftHand("Device registered");
            }
        }
        else
        {
            Debug.LogError("Unable to register Left Hand Controller. Please make sure that your VR Headset is turned on and the controllers are connected.");
            DebugLogLeftHand("Unable to register");
        }
    }

    void CheckLeftHandedInputs()
    {
        bool triggerValue;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            string debugText = LeftHandControllers[0].name + "\nTrigger button is pressed.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }

        bool gripValue;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
        {
            string debugText = LeftHandControllers[0].name + "\nGrip button is pressed.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }

        bool primaryButton;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton) && primaryButton)
        {
            string debugText = LeftHandControllers[0].name + "\nPrimary button (X) is pressed.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }

        bool secondaryButton;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton) && secondaryButton)
        {
            string debugText = LeftHandControllers[0].name + "\nSecondary button (Y) is pressed.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }

        bool menuButton;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuButton) && menuButton)
        {
            string debugText = LeftHandControllers[0].name + "\nMenu button is pressed.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }

        bool primary2DAxisClick;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            string debugText = LeftHandControllers[0].name + "\nJoystick click button is pressed.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }

        bool primary2DAxisTouch;
        if (LeftHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            string debugText = LeftHandControllers[0].name + "\nJoystick is moved.";

            Debug.Log(debugText);
            m_leftHandDebugCanvas.CCText.text = debugText;
        }
    }

    void CheckRightHandedInputs()
    {
        bool triggerValue;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            Debug.Log(RightHandControllers[0].name + " - Trigger button is pressed.");
        }

        bool gripValue;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
        {
            Debug.Log(RightHandControllers[0].name + " - Grip button is pressed.");
        }

        bool primaryButton;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton) && primaryButton)
        {
            Debug.Log(RightHandControllers[0].name + " - Primary button (A) is pressed.");
        }

        bool secondaryButton;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton) && secondaryButton)
        {
            Debug.Log(RightHandControllers[0].name + " - Secondary button (B) is pressed.");
        }

        bool menuButton;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuButton) && menuButton)
        {
            Debug.Log(RightHandControllers[0].name + " - Menu button is pressed.");
        }

        bool primary2DAxisClick;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            Debug.Log(RightHandControllers[0].name + " - Joystick click button is pressed.");
        }

        bool primary2DAxisTouch;
        if (RightHandControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            Debug.Log(RightHandControllers[0].name + " - Joystick is moved.");
        }
    }

    public void DebugLogInGame(string message)
    {
        m_inGameDebugCanvas.CCText.text = message;
    }

    public void DebugLogRightHand(string message)
    {
        m_rightHandDebugCanvas.CCText.text = message;
    }

    public void DebugLogLeftHand(string message)
    {
        m_leftHandDebugCanvas.CCText.text = message;
    }
}
