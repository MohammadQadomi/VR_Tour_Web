using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust the rotation speed
    public float smoothFactor = 0.5f; // Adjust the smoothness factor

    public bool isRotating = false;
    private float targetRotationX, targetRotationY;
    private float currentRotationX, currentRotationY;

    //public float oldRotation;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            targetRotationX = currentRotationX;
            targetRotationY = currentRotationY;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            targetRotationX -= -Input.GetAxis("Mouse Y") * rotationSpeed;
            targetRotationY += -Input.GetAxis("Mouse X") * rotationSpeed;
            targetRotationX = Mathf.Clamp(targetRotationX, -90f, 90f);

            // Smoothly interpolate the rotations
            currentRotationX = Mathf.Lerp(currentRotationX, targetRotationX, smoothFactor);
            currentRotationY = Mathf.Lerp(currentRotationY, targetRotationY, smoothFactor);

            // Apply the rotation to the camera
            transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        }
    }

    public void UpdateRotationValues()
    {
        currentRotationY = transform.eulerAngles.y;
        currentRotationX = transform.eulerAngles.x;
    }

    public void RotateCameraOnUpVector(float rotation)
    {
        transform.eulerAngles = new Vector3(0, /*transform.rotation.eulerAngles.y - oldRotation +*/ rotation, 0);
        UpdateRotationValues();
        //oldRotation = rotation;
    }

    public void StopRotating()
    {
        Invoke("setIsRotating", 0.01f);
    }

    void setIsRotating()
    {
        isRotating = false;
    }
}
