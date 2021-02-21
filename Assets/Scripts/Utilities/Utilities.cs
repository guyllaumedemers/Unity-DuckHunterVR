using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static void GetCameraTransformAndRotation(GameObject gameObject, Camera camera)
    {
        Vector3 angle = camera.transform.rotation.eulerAngles;
        double rad = (Math.PI / 180) * angle.y;
        int radius = 5; /// rotation radius around the camera yAxis
        double z = radius * Math.Cos(rad);
        double x = radius * Math.Sin(rad); /// zAxis equals vector.forward
        gameObject.transform.position = camera.transform.position + new Vector3((float)x, 0, (float)z);
        gameObject.transform.rotation = Quaternion.Euler(0, angle.y, 0);
    }
}
