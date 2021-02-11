using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputReactorWeapon : MonoBehaviour
{
    [SerializeField]
    private XRInputWatcher _xRInputWatcher;
    private XRGrabInteractable _xRGrabInteractable;
    [SerializeField]
    private bool _primaryIsPressed = false, _secondaryIsPressed = false, _triggerIsPressed = false;

    private IWeapon _iWeapon;

    private void Start()
    {
        _iWeapon = (IWeapon)GetComponent(typeof(IWeapon));
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();       
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        _primaryIsPressed = pressed;

        if (pressed)
        {
            _iWeapon.DropClip();
        }
    }

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

    public void onTriggerButtonEvent(bool pressed)
    {
        _triggerIsPressed = pressed;
        
        if (pressed)
        {
            if(_iWeapon.RateOfFire > 0)
            {
                _iWeapon.Shoot();
            }
        }
    }

    public void AssignWatcher()
    {
        XRBaseInteractor xRBaseInteractor = _xRGrabInteractable.selectingInteractor;
        _xRInputWatcher = GameObject.Find(xRBaseInteractor.gameObject.name).GetComponent<XRInputWatcher>();

        _xRInputWatcher.primaryButtonPressEvent.AddListener(onPrimaryButtonEvent);
        _xRInputWatcher.secondaryButtonPressEvent.AddListener(onSecondaryButtonEvent);
        _xRInputWatcher.triggerButtonPressEvent.AddListener(onTriggerButtonEvent);
    }

    public void ClearWatcher()
    {
        _xRInputWatcher.primaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher.secondaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher.triggerButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher = null;
    }
}
