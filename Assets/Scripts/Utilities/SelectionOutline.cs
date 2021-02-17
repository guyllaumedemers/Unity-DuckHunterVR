using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectionOutline : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _originalColor;
    public bool _isSelected;

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
        }
    }

    public void Highlight()
    {
        _meshRenderer.material.color = Color.green;
    }

    public void RemoveHighlight()
    {
        _meshRenderer.material.color = _originalColor;
    }

    public void IsSelected()
    {
        _isSelected = true;
    }

    public void IsNotSelected()
    {
        _isSelected = false;
        Highlight();
    }
}
