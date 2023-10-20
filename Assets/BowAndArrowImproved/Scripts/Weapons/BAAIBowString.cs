using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BAAIBowString : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform point1, point2;

    private void Start()
    {
        UpdateString(null);
    }

    public void UpdateString(Vector3? midPoint)
    {
        Vector3[] linePoints = new Vector3[midPoint == null ? 2 : 3];
        
        linePoints[0] = point1.localPosition;

        if (midPoint != null)
        {
            linePoints[1] = transform.InverseTransformPoint(midPoint.Value);
        }

        linePoints[^1] = point2.localPosition;

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}
