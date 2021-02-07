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
    public List<InputDevice> m_leftHandControllers;
    public List<InputDevice> m_rightHandControllers;

    public InputDevice m_leftHandController;
    public InputDevice m_rightHandController;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        RegisterControllers();
    }

    void RegisterControllers()
    {
        m_leftHandControllers = new List<InputDevice>();
        m_rightHandControllers = new List<InputDevice>();

        //Left Hand Controller
        InputDeviceCharacteristics leftHandedCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(leftHandedCharacteristics, m_leftHandControllers);

        if (m_leftHandControllers.Count > 0)
        {
            m_leftHandController = m_leftHandControllers[0];

            if (InputDebugger.Instance.m_inputDebugEnabled)
            {
                Debug.Log(string.Format("Registered device name '{0}' ", m_leftHandController.name));
                InputDebugger.Instance.DebugLogLeftHand("Left device registered");
            }            
        }
        else
        {
            Debug.LogError("Unable to register Left Hand Controller. Please make sure that your VR Headset is turned on and the controllers are connected.");
            InputDebugger.Instance.DebugLogLeftHand("Unable to register");
        }

        //Right Hand Controller
        InputDeviceCharacteristics rightHandedCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightHandedCharacteristics, m_rightHandControllers);

        if (m_rightHandControllers.Count > 0)
        {
            m_rightHandController = m_rightHandControllers[0];

            if (InputDebugger.Instance.m_inputDebugEnabled)
            {
                Debug.Log(string.Format("Registered device name '{0}' ", m_rightHandController.name));
                InputDebugger.Instance.DebugLogRightHand("Right device registered");
            }            
        }
        else
        {
            Debug.LogError("Unable to register Right Hand Controller. Please make sure that your VR Headset is turned on and the controllers are connected.");
            InputDebugger.Instance.DebugLogRightHand("Unable to register");
        }
    }
}
