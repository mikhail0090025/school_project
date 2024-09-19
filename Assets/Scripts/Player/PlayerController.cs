using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MouseRotate))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] MouseRotate MouseRotate;
    [SerializeField] float speed = 4f;
    [SerializeField] Transform HorizontalPoint;
    // Start is called before the first frame update
    void Start()
    {
        MouseRotate = GetComponent<MouseRotate>();
        MouseRotate.RotateY = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (WindowsManager.AreOpenedWindows) return;
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput > 0)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else if (verticalInput < 0)
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }

        if (horizontalInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, Time.deltaTime * speed);
        }
        else if (horizontalInput < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, -Time.deltaTime * speed);
        }
    }
}
