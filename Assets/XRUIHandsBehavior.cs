using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRUIHandsBehavior : MonoBehaviour
{
    #region Singleton
    private static XRUIHandsBehavior instance;

    private XRUIHandsBehavior() { }

    public static XRUIHandsBehavior Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XRUIHandsBehavior();
            }
            return instance;
        }
    }
    #endregion

    public XRRayInteractor leftHandUIRayInteractor, rightHandUIRayInteractor;
    public XRInteractorLineVisual leftHandUILineVisual, rightHandUILineVisual;
    public LayerMask leftHandUIMaskDefault, rightHandUIMaskDefault;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        leftHandUIRayInteractor = GameObject.FindGameObjectWithTag("LeftHandUI").GetComponent<XRRayInteractor>();
        rightHandUIRayInteractor = GameObject.FindGameObjectWithTag("RightHandUI").GetComponent<XRRayInteractor>();

        leftHandUILineVisual = GameObject.FindGameObjectWithTag("LeftHandUI").GetComponent<XRInteractorLineVisual>();
        rightHandUILineVisual = GameObject.FindGameObjectWithTag("RightHandUI").GetComponent<XRInteractorLineVisual>();

        leftHandUIMaskDefault = leftHandUIRayInteractor.raycastMask;
        rightHandUIMaskDefault = rightHandUIRayInteractor.raycastMask;
    }

    public void ItemIsHeld(string interactorName)
    {
        if (interactorName.Contains("Left") && leftHandUILineVisual.enabled == true)
        {
            leftHandUILineVisual.enabled = false;
            leftHandUIRayInteractor.raycastMask = 0;
        }

        if (interactorName.Contains("Right") && rightHandUILineVisual.enabled == true)
        {
            rightHandUILineVisual.enabled = false;
            rightHandUIRayInteractor.raycastMask = 0;
        }
    }

    public void ItemIsNotHeld(string interactorName)
    {
        if (interactorName.Contains("Left") && leftHandUILineVisual.enabled == false)
        {
            leftHandUILineVisual.enabled = true;
            leftHandUIRayInteractor.raycastMask = leftHandUIMaskDefault;
        }

        if (interactorName.Contains("Right") && rightHandUILineVisual.enabled == false)
        {
            rightHandUILineVisual.enabled = true;
            rightHandUIRayInteractor.raycastMask = rightHandUIMaskDefault;
        }
    }
}
