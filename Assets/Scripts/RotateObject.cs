using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed for X-axis
    [SerializeField] private float xRotationSpeed = 30f; // Degrees per second

    void Update()
    {
        // Calculate rotation for this frame
        float xRotation = xRotationSpeed * Time.deltaTime;

        // Apply the rotation to the object
        transform.Rotate(xRotation, 0f, 0f);
    }
}
