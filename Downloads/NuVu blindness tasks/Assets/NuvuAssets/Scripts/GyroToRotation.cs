using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroToRotation : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;

    bool moved = false;

    void Start()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.01f;
        transform.rotation = Input.gyro.attitude;
        Application.targetFrameRate = 60;

        if (!SystemInfo.supportsGyroscope)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
            UpdateAbsolute();
        else
            UpdateMouse();
    }

    void UpdateAbsolute()
    {
        transform.rotation = Input.gyro.attitude;
        transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
        transform.Rotate(90.0f, 180.0f, 0.0f, Space.World);
    }

    Vector3 convertRotation(Vector3 gyro)
    {
        return new Vector3(-gyro.x, -gyro.y, gyro.z);
    }

    void UpdateRelative()
    {
        Quaternion rotation = transform.rotation;
        Vector3 cameraRotation = convertRotation(Input.gyro.rotationRateUnbiased);
        rotation *= Quaternion.Euler(cameraRotation);
        transform.rotation = rotation;
    }

    void UpdateMouse()
    {
        Vector3 currentEuler = transform.rotation.eulerAngles;
        Vector3 deltaEuler = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f);

        if (!moved)
        {
            if (deltaEuler != Vector3.zero)
                moved = true;
            return;
        }

        currentEuler += mouseSensitivity * deltaEuler;
        if (currentEuler.x < 180.0f)
            currentEuler.x = Mathf.Min(currentEuler.x, 80.0f);
        else
            currentEuler.x = Mathf.Max(currentEuler.x, 280.0f);
        transform.rotation = Quaternion.Euler(currentEuler);
    }
}
