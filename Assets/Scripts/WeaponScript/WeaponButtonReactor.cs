using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponButtonReactor : MonoBehaviour
{
    public ButtonWatcher watcher;
    private XRGrabInteractable xRGrabInteractable;
    public bool PrimaryIsPressed = false;
    public bool SecondaryIsPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        xRGrabInteractable = GetComponent<XRGrabInteractable>();              
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        PrimaryIsPressed = pressed;

        if (pressed)
        {
            Debug.Log("WeaponButtonReactor Primary Pressed");
        }
    }

    public void onSecondaryButtonEvent(bool pressed)
    {
        SecondaryIsPressed = pressed;

        if (pressed)
        {
            Debug.Log("WeaponButtonReactor Secondary Pressed");
        }
    }

    public void AssignWatcher()
    {
        XRBaseInteractor xRBaseInteractor = xRGrabInteractable.selectingInteractor;
        GameObject interactor = xRBaseInteractor.gameObject;
        watcher = GameObject.Find(interactor.gameObject.name).GetComponent<ButtonWatcher>();

        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
        watcher.secondaryButtonPress.AddListener(onSecondaryButtonEvent);
    }

    public void ClearWatcher()
    {
        watcher.primaryButtonPress.RemoveAllListeners();
        watcher.secondaryButtonPress.RemoveAllListeners();
        watcher = null;
    }
}
