using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputDebugger : MonoBehaviour
{
    #region Singleton
    private static InputDebugger instance;

    private InputDebugger() { }

    public static InputDebugger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputDebugger();
            }
            return instance;
        }
    }
    #endregion

    [Tooltip("If InputDebugEnable is True, In-Game Debug Canvases will be shown and button presses will be output to the Console and also in the In-Game Debug Canvases")]
    public bool m_inputDebugEnabled = false;

    [Header("Debug Canvases")]
    public CCCanvas m_inGameDebugCanvas;
    public CCCanvas m_leftHandDebugCanvas;
    public CCCanvas m_rightHandDebugCanvas;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DebugLogInGame("Debug line 1\n" + "Debug line 2");
    }

    void Update()
    {
        if (m_inputDebugEnabled)
        {
            m_inGameDebugCanvas.gameObject.SetActive(true);
            m_leftHandDebugCanvas.gameObject.SetActive(true);
            m_rightHandDebugCanvas.gameObject.SetActive(true);
            DebugInputLeftHand();
            DebugInputRightHand();
        }
        else
        {
            m_inGameDebugCanvas.gameObject.SetActive(false);
            m_leftHandDebugCanvas.gameObject.SetActive(false);
            m_rightHandDebugCanvas.gameObject.SetActive(false);
        }
    }

    void DebugInputLeftHand()
    {
        bool triggerPressed;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            string debugText = GameController.Instance.m_leftHandControllers[0].name + "\nTrigger button is pressed.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }

        bool gripPressed;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed) && gripPressed)
        {
            string debugText = GameController.Instance.m_leftHandController.name + "\nGrip button is pressed.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }

        bool primaryButtonPressed;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
        {
            string debugText = GameController.Instance.m_leftHandController.name + "\nPrimary button (X) is pressed.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }

        bool secondaryButtonPressed;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonPressed) && secondaryButtonPressed)
        {
            string debugText = GameController.Instance.m_leftHandController.name + "\nSecondary button (Y) is pressed.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }

        bool menuButtonPressed;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
        {
            string debugText = GameController.Instance.m_leftHandController.name + "\nMenu button is pressed.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }

        bool primary2DAxisClick;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            string debugText = GameController.Instance.m_leftHandController.name + "\nJoystick click button is pressed.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }

        bool primary2DAxisTouch;
        if (GameController.Instance.m_leftHandController.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            string debugText = GameController.Instance.m_leftHandController.name + "\nJoystick is moved.";

            Debug.Log(debugText);
            DebugLogLeftHand(debugText);
        }
    }

    void DebugInputRightHand()
    {
        bool triggerPressed;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Trigger button is pressed.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }

        bool gripPressed;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripPressed) && gripPressed)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Grip button is pressed.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }

        bool primaryButtonPressed;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Primary button (A) is pressed.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }

        bool secondaryButtonPressed;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButtonPressed) && secondaryButtonPressed)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Secondary button (B) is pressed.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }

        bool menuButtonPressed;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Menu button is pressed.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }

        bool primary2DAxisClick;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Joystick click button is pressed.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }

        bool primary2DAxisTouch;
        if (GameController.Instance.m_rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            string debugText = GameController.Instance.m_rightHandController.name + " - Joystick is moved.";

            Debug.Log(debugText);
            DebugLogRightHand(debugText);
        }
    }

    public void DebugLogInGame(string message)
    {
        if (m_inputDebugEnabled)
        {
            m_inGameDebugCanvas.CCText.text = message;
        }
    }

    public void DebugLogRightHand(string message)
    {
        if (m_inputDebugEnabled)
        {
            m_rightHandDebugCanvas.CCText.text = message;
        }
    }

    public void DebugLogLeftHand(string message)
    {
        if (m_inputDebugEnabled)
        {
            m_leftHandDebugCanvas.CCText.text = message;
        }
    }
}
