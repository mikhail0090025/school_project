using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MouseRotate))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] MouseRotate MouseRotate;
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
        
    }
}
