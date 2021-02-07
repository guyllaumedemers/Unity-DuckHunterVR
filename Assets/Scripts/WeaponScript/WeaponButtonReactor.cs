using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponButtonReactor : MonoBehaviour
{
    public ButtonWatcher m_buttonWatcher;
    private XRGrabInteractable m_xRGrabInteractable;
    public bool PrimaryIsPressed = false;
    public bool SecondaryIsPressed = false;

    private Renderer m_renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        PrimaryIsPressed = pressed;

        if (pressed)
        {
            m_renderer.material.color = new Color(0, 255, 0);
            //Debug.Log("WeaponButtonReactor Primary Pressed");
        }
    }

    public void onSecondaryButtonEvent(bool pressed)
    {
        SecondaryIsPressed = pressed;

        if (pressed)
        {
            m_renderer.material.color = new Color(255, 0, 0);
            //Debug.Log("WeaponButtonReactor Secondary Pressed");
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
