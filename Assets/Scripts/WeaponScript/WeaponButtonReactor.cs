using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponButtonReactor : MonoBehaviour
{
    public ButtonWatcher m_buttonWatcher;
    private XRGrabInteractable m_xRGrabInteractable;
    public bool m_primaryIsPressed = false;
    public bool m_secondaryIsPressed = false;

    private Renderer m_renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        m_primaryIsPressed = pressed;

        if (pressed)
        {
            m_renderer.material.color = new Color(0, 255, 0);
            
            if (InputDebugger.Instance.m_inputDebugEnabled)
            {
                string debugMessage = name + " Primary Button Event Fired";
                Debug.Log(debugMessage);
                InputDebugger.Instance.DebugLogInGame(debugMessage);
            }
        }
    }

    public void onSecondaryButtonEvent(bool pressed)
    {
        m_secondaryIsPressed = pressed;

        if (pressed)
        {
            m_renderer.material.color = new Color(255, 0, 0);

            if (InputDebugger.Instance.m_inputDebugEnabled)
            {
                string debugMessage = name + " Secondary Button Event Fired";
                Debug.Log(debugMessage);
                InputDebugger.Instance.DebugLogInGame(debugMessage);
            }
        }
    }

    public void AssignWatcher()
    {
        XRBaseInteractor xRBaseInteractor = m_xRGrabInteractable.selectingInteractor;
        GameObject interactor = xRBaseInteractor.gameObject;
        m_buttonWatcher = GameObject.Find(interactor.gameObject.name).GetComponent<ButtonWatcher>();

        m_buttonWatcher.e_primaryButtonPress.AddListener(onPrimaryButtonEvent);
        m_buttonWatcher.e_secondaryButtonPress.AddListener(onSecondaryButtonEvent);
    }

    public void ClearWatcher()
    {
        m_buttonWatcher.e_primaryButtonPress.RemoveAllListeners();
        m_buttonWatcher.e_secondaryButtonPress.RemoveAllListeners();
        m_buttonWatcher = null;
    }
}
