using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Profiling;
using UnityEngine;

[RequireComponent(typeof(MouseRotate))] // Vyžaduje komponentu MouseRotate
public class PlayerController : MonoBehaviour
{
    [SerializeField] MouseRotate MouseRotate;   // Odkaz na komponentu MouseRotate
    [SerializeField] float speed = 4f;           // Rychlost chůze
    [SerializeField] float runSpeed = 7f;        // Rychlost běhu
    [SerializeField] float crouchSpeed = 2f;     // Rychlost dřepu
    [SerializeField] float jump = 50f;           // Výška skoku
    [SerializeField] Transform HorizontalPoint;   // Bod pro horizontální pohyb
    [SerializeField] Animator CameraAnimator;     // Odkaz na animátor kamery
    const float jumpFactor = 0.1f;               // Faktor pro výšku skoku
    Rigidbody Rigidbody;                          // Odkaz na Rigidbody komponentu
    CapsuleCollider cc;                          // Odkaz na CapsuleCollider komponentu

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>(); // Získání Rigidbody komponenty
        MouseRotate = GetComponent<MouseRotate>(); // Získání MouseRotate komponenty
        MouseRotate.RotateY = false; // Zakáže rotaci kolem osy Y
        Cursor.lockState = CursorLockMode.Locked; // Zamknutí kurzoru
        Cursor.visible = false; // Skrytí kurzoru
        cc = GetComponent<CapsuleCollider>(); // Získání CapsuleCollider komponenty
    }

    // Update is called once per frame
    void Update()
    {
        // Pokud jsou otevřena nějaká okna, ukončí aktualizaci
        if (WindowsManager.AreOpenedWindows) return;

        // Získání vstupů z klávesnice
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float crouchInput = Input.GetAxis("Crouch");
        float jumpInput = Input.GetAxis("Jump");

        float curr_speed; // Aktuální rychlost

        // Určení aktuální rychlosti podle vstupu
        if (crouchInput > 0)
            curr_speed = crouchSpeed; // Dřep
        else if (Input.GetKey(KeyCode.LeftShift))
            curr_speed = runSpeed; // Běh
        else
            curr_speed = speed; // Chůze

        // Nastavení animací podle vstupů
        CameraAnimator.SetBool("Run", Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)));
        CameraAnimator.SetBool("Aim", Input.GetMouseButton(1));

        // Nastavení výšky kolize podle dřepu
        cc.height = crouchInput > 0 ? 1 : 2;

        // Pohyb vpřed a vzad
        if (verticalInput > 0)
        {
            transform.position += transform.forward * Time.deltaTime * curr_speed; // Pohyb vpřed
        }
        else if (verticalInput < 0)
        {
            transform.position -= transform.forward * Time.deltaTime * curr_speed; // Pohyb vzad
        }

        // Pohyb do stran
        if (horizontalInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, Time.deltaTime * curr_speed); // Pohyb doprava
        }
        else if (horizontalInput < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, HorizontalPoint.position, -Time.deltaTime * curr_speed); // Pohyb doleva
        }

        // Skok
        if (jumpInput > 0)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.distance < 2f) transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * jump * jumpFactor); // Skok
            }
        }
    }
}
