using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public bool RotateX = true;
    public bool RotateY = true;

    public float sensitivityX = 100f;
    public float sensitivityY = 100f;

    public float minX = -90f;
    public float maxX = 90f;
    public float minY = -90f;
    public float maxY = 90f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        if (minX == 0f) minX = float.NegativeInfinity;
        if (minY == 0f) minY = float.NegativeInfinity;
        if (maxX == 0f) maxX = float.PositiveInfinity;
        if (maxY == 0f) maxY = float.PositiveInfinity;
    }
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        // Rotate around the X axis (horizontal rotation, Y axis on screen)
        if (RotateX)
        {
            rotationX += mouseX;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
        }

        // Rotate around the Y axis (vertical rotation, X axis on screen)
        if (RotateY)
        {
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
        }

        // Apply the rotation to the player object
        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }
}