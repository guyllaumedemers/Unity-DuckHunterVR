using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputReactorMenuUI : MonoBehaviour
{
    [SerializeField]
    private XRInputWatcher _xRInputWatcherLeft;
    [SerializeField]
    private XRInputWatcher _xRInputWatcherRight;
    [SerializeField]
    private bool _primaryIsPressed = false, _secondaryIsPressed = false, _menuIsPressed = false;

    private void Start()
    {
        AssignWatcher();
    }
    /// <summary>
    /// Confirm button
    /// </summary>
    /// <param name="pressed"></param>
    public void onPrimaryButtonEvent(bool pressed)
    {
        _primaryIsPressed = pressed;

        if (pressed)
        {
            //if (XRInputDebugger.Instance.inputDebugEnabled)
            //{
            //    string debugMessage = name + " Primary Button Event Fired";
            //    Debug.Log(debugMessage);
            //    XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            //}
        }
    }
    /// <summary>
    /// Back button
    /// </summary>
    /// <param name="pressed"></param>
    public void onSecondaryButtonEvent(bool pressed)
    {
        _secondaryIsPressed = pressed;

        if (pressed)
        {
            /*if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                string debugMessage = name + " Secondary Button Event Fired";
                Debug.Log(debugMessage);
                XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            }*/
        }
    }

    public void onMenuButtonEvent(bool pressed)
    {
        _menuIsPressed = pressed;

        if (pressed)
        {
            if (PlayerMenuUIScript.Instance.IsInGameScene() && PlayerMenuUIScript.Instance.IsSettingsMenuUIActive() == false && PlayerMenuUIScript.Instance.IsStatsMenuUIActive() == false)
            {
                PlayerMenuUIScript.Instance.ActivateInGameMenuUI();
            }
        }
    }

    public void AssignWatcher()
    {
        _xRInputWatcherLeft = GameObject.FindGameObjectWithTag("LeftController").GetComponent<XRInputWatcher>();

        _xRInputWatcherLeft.primaryButtonPressEvent.AddListener(onPrimaryButtonEvent);
        _xRInputWatcherLeft.secondaryButtonPressEvent.AddListener(onSecondaryButtonEvent);
        _xRInputWatcherLeft.menuButtonPressEvent.AddListener(onMenuButtonEvent);

        _xRInputWatcherRight = GameObject.FindGameObjectWithTag("RightController").GetComponent<XRInputWatcher>();

        _xRInputWatcherRight.primaryButtonPressEvent.AddListener(onPrimaryButtonEvent);
        _xRInputWatcherRight.secondaryButtonPressEvent.AddListener(onSecondaryButtonEvent);
    }

    public void ClearWatcher()
    {
        _xRInputWatcherLeft.primaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcherLeft.secondaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcherLeft = null;
    }
}
