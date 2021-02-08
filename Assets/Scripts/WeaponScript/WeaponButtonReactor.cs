using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponButtonReactor : MonoBehaviour
{
    public XRButtonWatcher buttonWatcher;
    private XRGrabInteractable _xRGrabInteractable;
    public bool primaryIsPressed = false;
    public bool secondaryIsPressed = false;

    private Renderer m_renderer;

    // Start is called before the first frame update
    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        primaryIsPressed = pressed;

        if (pressed)
        {
            m_renderer.material.color = new Color(0, 255, 0);
            
            if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                string debugMessage = name + " Primary Button Event Fired";
                Debug.Log(debugMessage);
                XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            }
        }
    }

    public void onSecondaryButtonEvent(bool pressed)
    {
        secondaryIsPressed = pressed;

        if (pressed)
        {
            m_renderer.material.color = new Color(255, 0, 0);

            if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                string debugMessage = name + " Secondary Button Event Fired";
                Debug.Log(debugMessage);
                XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            }
        }
    }

    public void AssignWatcher()
    {
        XRBaseInteractor xRBaseInteractor = _xRGrabInteractable.selectingInteractor;
        GameObject interactor = xRBaseInteractor.gameObject;
        buttonWatcher = GameObject.Find(interactor.gameObject.name).GetComponent<XRButtonWatcher>();

        buttonWatcher.primaryButtonPressEvent.AddListener(onPrimaryButtonEvent);
        buttonWatcher.secondaryButtonPressEvent.AddListener(onSecondaryButtonEvent);
    }

    public void ClearWatcher()
    {
        buttonWatcher.primaryButtonPressEvent.RemoveAllListeners();
        buttonWatcher.secondaryButtonPressEvent.RemoveAllListeners();
        buttonWatcher = null;
    }
}
