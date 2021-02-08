using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRInputDebugger : MonoBehaviour
{
    #region Singleton
    private static XRInputDebugger instance;

    private XRInputDebugger() { }

    public static XRInputDebugger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XRInputDebugger();
            }
            return instance;
        }
    }
    #endregion

    [Tooltip("If InputDebugEnable is True, In-Game Debug Canvases will be shown and button presses will be output to the Console and also in the In-Game Debug Canvases")]
    public bool inputDebugEnabled = false;

    [Header("Debug Canvases")]
    public CCCanvas inGameDebugCanvas;
    public CCCanvas leftHandDebugCanvas;
    public CCCanvas rightHandDebugCanvas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DebugLogInGame("Debug line 1\n" + "Debug line 2");
    }

    private void Update()
    {
        if (inputDebugEnabled)
        {
            inGameDebugCanvas.gameObject.SetActive(true);
            leftHandDebugCanvas.gameObject.SetActive(true);
            rightHandDebugCanvas.gameObject.SetActive(true);
            DebugInputLeftHand();
            DebugInputRightHand();
        }
        else
        {
            inGameDebugCanvas.gameObject.SetActive(false);
            leftHandDebugCanvas.gameObject.SetActive(false);
            rightHandDebugCanvas.gameObject.SetActive(false);
        }
    }

    private void DebugInputLeftHand()
    {
        bool triggerPressed;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nTrigger button is pressed.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }

        bool gripPressed;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed) && gripPressed)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nGrip button is pressed.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }

        bool primaryButtonPressed;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nPrimary button (X) is pressed.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }

        bool secondaryButtonPressed;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonPressed) && secondaryButtonPressed)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nSecondary button (Y) is pressed.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }

        bool menuButtonPressed;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nMenu button is pressed.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }

        bool primary2DAxisClick;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nJoystick click button is pressed.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }

        bool primary2DAxisTouch;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            string debugMessage = XRInputManager.Instance.leftHandController.name + "\nJoystick is moved.";

            Debug.Log(debugMessage);
            DebugLogLeftHand(debugMessage);
        }
    }

    private void DebugInputRightHand()
    {
        bool triggerPressed;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Trigger button is pressed.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }

        bool gripPressed;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripPressed) && gripPressed)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Grip button is pressed.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }

        bool primaryButtonPressed;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Primary button (A) is pressed.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }

        bool secondaryButtonPressed;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButtonPressed) && secondaryButtonPressed)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Secondary button (B) is pressed.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }

        bool menuButtonPressed;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Menu button is pressed.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }

        bool primary2DAxisClick;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Joystick click button is pressed.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }

        bool primary2DAxisTouch;
        if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            string debugMessage = XRInputManager.Instance.rightHandController.name + " - Joystick is moved.";

            Debug.Log(debugMessage);
            DebugLogRightHand(debugMessage);
        }
    }

    public void DebugLogInGame(string debugMessage)
    {
        if (inputDebugEnabled)
        {
            inGameDebugCanvas.CCText.text = debugMessage;
        }
    }

    public void DebugLogRightHand(string debugMessage)
    {
        if (inputDebugEnabled)
        {
            rightHandDebugCanvas.CCText.text = debugMessage;
        }
    }

    public void DebugLogLeftHand(string debugMessage)
    {
        if (inputDebugEnabled)
        {
            leftHandDebugCanvas.CCText.text = debugMessage;
        }
    }
}
