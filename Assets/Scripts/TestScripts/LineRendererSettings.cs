using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    private Vector3[] points = new Vector3[2];
    private const int length = 100;
    public LayerMask layerMask;

    public void Start()
    {
        InitializeVectorArray();
        // retrieve the LineRenderer component attached to the gameobject holding this script
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        // retrieve the starting point and the ending point of the line rendering
        points[0] = Vector3.zero;
        points[1] = transform.position + (Vector3.forward * length);
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }

    public void Update()
    {
        AlignLineRenderer(lineRenderer);
    }

    public void InitializeVectorArray()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Vector3.zero;
        }
    }

    public void AlignLineRenderer(LineRenderer lineRenderer)
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, Vector3.forward, out raycastHit, length, LayerMask.GetMask(layerMask.ToString())))
        {
            Debug.Log("Hit");
            // if you hit an object on the layer mask => you stop the ray from going thru by setting the end point at the position of the object hit
            points[1] = raycastHit.point;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        else
        {
            // set the end point to the original lenght it should have if it didnt hit aything
            points[1] = transform.position + (Vector3.forward * length);
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        // update the lineRenderer points position
        lineRenderer.SetPositions(points);
        lineRenderer.material.color = lineRenderer.startColor;
    }
}
