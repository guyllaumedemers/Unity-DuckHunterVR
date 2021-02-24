using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputReactorWeapon : MonoBehaviour
{
    [SerializeField]
    private XRInputWatcher _xRInputWatcher;
    private XRGrabInteractable _xRGrabInteractable;
    [Header("Debug")]
    [SerializeField] private bool _primaryIsPressed = false;
    [SerializeField] private bool _secondaryIsPressed = false;
    [SerializeField] private bool _triggerIsPressed = false;

    private IWeapon _iWeapon;
    private SelectionOutline _selectionOutline;

    private void Start()
    {
        _iWeapon = (IWeapon)GetComponent(typeof(IWeapon));
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();

        if (gameObject.GetComponent<SelectionOutline>() != null)
            _selectionOutline = GetComponent<SelectionOutline>();
    }

    private void Update()
    {
        if (_xRInputWatcher != null)
        {
            _selectionOutline.RemoveHighlight();
        }
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        _primaryIsPressed = pressed;

        if (pressed)
        {
            _iWeapon.DropMagazine();
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
            if (_iWeapon.ReactorTriggerShoot)
            {
                _iWeapon.Shoot();
            }
        }
    }

    public void AssignWatcher()
    {
        if (_selectionOutline != null)
            _selectionOutline.RemoveHighlight();

        XRBaseInteractor xRBaseInteractor = _xRGrabInteractable.selectingInteractor;
        _xRInputWatcher = GameObject.Find(xRBaseInteractor.gameObject.name).GetComponent<XRInputWatcher>();

        XRUIHandsBehavior.Instance.ItemIsHeld(_xRInputWatcher.name);

        _xRInputWatcher.primaryButtonPressEvent.AddListener(onPrimaryButtonEvent);
        _xRInputWatcher.secondaryButtonPressEvent.AddListener(onSecondaryButtonEvent);
        _xRInputWatcher.triggerButtonPressEvent.AddListener(onTriggerButtonEvent);
    }

    public void ClearWatcher()
    {
        if (_selectionOutline != null)
            _selectionOutline.Highlight();

        XRUIHandsBehavior.Instance.ItemIsNotHeld(_xRInputWatcher.name);

        _xRInputWatcher.primaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher.secondaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher.triggerButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher = null;
    }
}
