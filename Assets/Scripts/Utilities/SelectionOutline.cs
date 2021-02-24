using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionOutline : MonoBehaviour
{
    public MeshRenderer _meshRenderer;
    private Color _originalColor;
    private bool _isSelected;
    private XRBaseInteractor _xRBaseInteractor;

    private void Start()
    {
        _isSelected = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.color;
    }

    private void Update()
    {
        if (_isSelected)
        {
            RemoveHighlight();
            XRUIHandsBehavior.Instance.ItemIsHeld(_xRBaseInteractor.name);
        }
    }

    public void Highlight()
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = Color.green;
    }

    public void RemoveHighlight()
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = _originalColor;
    }

    public void IsSelected()
    {
        _isSelected = true;
        _xRBaseInteractor = GetComponent<XRGrabInteractable>().selectingInteractor;

        XRUIHandsBehavior.Instance.ItemIsHeld(_xRBaseInteractor.name);
    }

    public void IsNotSelected()
    {
        _isSelected = false;
        
        XRUIHandsBehavior.Instance.ItemIsNotHeld(_xRBaseInteractor.name);

        Highlight();
    }
}
