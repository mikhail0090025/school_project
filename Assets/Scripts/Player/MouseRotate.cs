using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public bool RotateX = true;                // Povolit rotaci kolem osy X
    public bool RotateY = true;                // Povolit rotaci kolem osy Y

    public float sensitivityX = 100f;          // Citlivost rotace kolem osy X
    public float sensitivityY = 100f;          // Citlivost rotace kolem osy Y

    public float minX = -90f;                  // Minimální úhel rotace kolem osy X
    public float maxX = 90f;                   // Maximální úhel rotace kolem osy X
    public float minY = -90f;                  // Minimální úhel rotace kolem osy Y
    public float maxY = 90f;                   // Maximální úhel rotace kolem osy Y

    private float rotationX = 0f;              // Aktuální rotace kolem osy X
    private float rotationY = 0f;              // Aktuální rotace kolem osy Y

    void Start()
    {
        // Nastavení nekonečných úhlů, pokud jsou minimální a maximální úhly 0
        if (minX == 0f) minX = float.NegativeInfinity;
        if (minY == 0f) minY = float.NegativeInfinity;
        if (maxX == 0f) maxX = float.PositiveInfinity;
        if (maxY == 0f) maxY = float.PositiveInfinity;
    }

    void Update()
    {
        // Pokud jsou otevřena nějaká okna, ukončí aktualizaci
        if (WindowsManager.AreOpenedWindows) return;
        RotatePlayer();  // Volá funkci pro rotaci hráče
    }

    void RotatePlayer()
    {
        // Získání vstupu z pohybu myši
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        // Rotace kolem osy X (horizontální rotace, osa Y na obrazovce)
        if (RotateX)
        {
            rotationX += mouseX;
            rotationX = Mathf.Clamp(rotationX, minX, maxX); // Omezí rotaci kolem osy X
        }

        // Rotace kolem osy Y (vertikální rotace, osa X na obrazovce)
        if (RotateY)
        {
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY); // Omezí rotaci kolem osy Y
        }

        // Aplikuje rotaci na objekt hráče
        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }
}
