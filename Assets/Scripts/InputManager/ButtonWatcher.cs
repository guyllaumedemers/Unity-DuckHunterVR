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
    public XRBaseController m_xRBaseController;
    public InputDevice m_inputDevice;

    public PrimaryButtonEvent e_primaryButtonPress;
    public SecondaryButtonEvent e_secondaryButtonPress;
    private bool m_primaryLastButtonState = false;
    private bool m_secondaryLastButtonState = false;

    private void Awake()
    {
        m_xRBaseController = GetComponent<XRBaseController>();

        if (e_primaryButtonPress == null)
        {
            e_primaryButtonPress = new PrimaryButtonEvent();
        }

        if (e_secondaryButtonPress == null)
        {
            e_secondaryButtonPress = new SecondaryButtonEvent();
        }
    }

    private void Start()
    {
        if (GameController.Instance.RightHandControllers.Count > 0)
        {
            if (m_xRBaseController.name.Contains("Right"))
            {
                m_inputDevice = GameController.Instance.RightHandControllers[0];
                //Debug.Log("ButtonWatcher has been linked to: " + m_inputDevice.name);
            }
        }

        if (GameController.Instance.LeftHandControllers.Count > 0)
        {
            if (m_xRBaseController.name.Contains("Left"))
            {
                m_inputDevice = GameController.Instance.LeftHandControllers[0];
                //Debug.Log("ButtonWatcher has been linked to: " + m_inputDevice.name);
            }
        }
    }

    private void Update()
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

        //Debug.Log(currentController.name + " Primary Button: " + primaryButtonState);
        //Debug.Log(currentController.name + " Secondary Button: " + secondaryButtonState);
    }
}
