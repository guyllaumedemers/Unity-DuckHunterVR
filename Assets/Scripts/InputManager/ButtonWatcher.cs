using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class PrimaryButtonEvent : UnityEvent<bool> { }
public class SecondaryButtonEvent : UnityEvent<bool> { }

public class ButtonWatcher : MonoBehaviour
{
    public XRBaseController m_xRBaseController;
    public InputDevice m_inputDevice;

    public PrimaryButtonEvent e_primaryButtonPress;
    public SecondaryButtonEvent e_secondaryButtonPress;
    private bool m_primaryLastButtonState = false;
    private bool m_secondaryLastButtonState = false;

    void Awake()
    {
        m_xRBaseController = GetComponent<XRBaseController>();

        if (e_primaryButtonPress == null)
            e_primaryButtonPress = new PrimaryButtonEvent();


        if (e_secondaryButtonPress == null)
            e_secondaryButtonPress = new SecondaryButtonEvent();
    }

    void Start()
    {
        if (m_xRBaseController.name.Contains("Left"))
        {
            if (GameController.Instance.m_leftHandController != null)
                m_inputDevice = GameController.Instance.m_leftHandController;

            if (m_inputDevice != null)
            {
                if (InputDebugger.Instance.m_inputDebugEnabled)
                    Debug.Log("ButtonWatcher has been linked to: " + m_inputDevice.name);
            }
            else
            {
                Debug.LogError("Unable to link ButtonWatcher to Left Controller.");
            }
        }

        if (m_xRBaseController.name.Contains("Right"))
        {
            if (GameController.Instance.m_rightHandController != null)
                m_inputDevice = GameController.Instance.m_rightHandController;

            if (m_inputDevice != null)
            {
                if (InputDebugger.Instance.m_inputDebugEnabled)
                    Debug.Log("ButtonWatcher has been linked to: " + m_inputDevice.name);
            }
            else
            {
                Debug.LogError("Unable to link ButtonWatcher to Right Controller.");
            }
        }
    }

    void Update()
    {
        bool primaryTempState = false;
        bool primaryButtonState = false;

        bool secondaryTempState = false;
        bool secondaryButtonState = false;

        primaryTempState = m_inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState || primaryTempState;

        if (primaryTempState != m_primaryLastButtonState)
        {
            e_primaryButtonPress.Invoke(primaryTempState);
            m_primaryLastButtonState = primaryTempState;
        }

        secondaryTempState = m_inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState) && secondaryButtonState || secondaryTempState;

        if (secondaryTempState != m_secondaryLastButtonState)
        {
            e_secondaryButtonPress.Invoke(secondaryTempState);
            m_secondaryLastButtonState = secondaryTempState;
        }
    }
}
