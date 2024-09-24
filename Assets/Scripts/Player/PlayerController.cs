using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MouseRotate))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] MouseRotate MouseRotate;
    [SerializeField] float speed = 4f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jump = 50f;
    [SerializeField] Transform HorizontalPoint;
    const float jumpFactor = 0.1f;
    Rigidbody Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
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
        float jumpInput = Input.GetAxis("Jump");

        if (verticalInput > 0)
        {
            transform.position += transform.forward * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);
        }
        else if (verticalInput < 0)
        {
            transform.position -= transform.forward * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);
        }

        if (horizontalInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed));
        }
        else if (horizontalInput < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, -Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed));
        }
        if(jumpInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * jump * jumpFactor);
        }
    }
}
