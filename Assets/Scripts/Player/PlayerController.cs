using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MouseRotate))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] MouseRotate MouseRotate;
    [SerializeField] float speed = 4f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float jump = 50f;
    [SerializeField] Transform HorizontalPoint;
    [SerializeField] Animator CameraAnimator;
    const float jumpFactor = 0.1f;
    Rigidbody Rigidbody;
    CapsuleCollider cc;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        MouseRotate = GetComponent<MouseRotate>();
        MouseRotate.RotateY = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WindowsManager.AreOpenedWindows) return;
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float crouchInput = Input.GetAxis("Crouch");
        float jumpInput = Input.GetAxis("Jump");
        float curr_speed;
        if (crouchInput > 0) curr_speed = crouchSpeed;
        else if (Input.GetKey(KeyCode.LeftShift)) curr_speed = runSpeed;
        else curr_speed = speed;
        CameraAnimator.SetBool("Run", Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)));
        CameraAnimator.SetBool("Aim", Input.GetMouseButton(1));
        cc.height = crouchInput > 0 ? 1 : 2;

        if (verticalInput > 0)
        {
            transform.position += transform.forward * Time.deltaTime * curr_speed;
        }
        else if (verticalInput < 0)
        {
            transform.position -= transform.forward * Time.deltaTime * curr_speed;
        }

        if (horizontalInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, Time.deltaTime * curr_speed);
        }
        else if (horizontalInput < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, -Time.deltaTime * curr_speed);
        }
        if(jumpInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * jump * jumpFactor);
        }
    }
}
