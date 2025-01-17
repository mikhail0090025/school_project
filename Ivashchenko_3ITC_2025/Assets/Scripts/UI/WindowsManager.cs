using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    public List<Window> windows; // Seznam oken

    // Start is called before the first frame update
    void Start()
    {
        // Vypni všechna okna při spuštění
        foreach (var item in windows) item.TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        // Zkontroluj vstupy pro každé okno
        foreach (var item in windows)
        {
            // Pokud je stisknuta klávesa přiřazená oknu
            if (Input.GetKeyDown(item.KeyCode))
            {
                item.Switch(); // Přepni stav okna
                // Změna stavu kurzoru podle toho, zda jsou otevřená okna
                Cursor.lockState = AreOpenedWindows ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = AreOpenedWindows; // Zobraz kurzor, pokud jsou okna otevřená
            }
        }
    }

    // Vlastnost pro kontrolu, zda jsou otevřená okna
    public static bool AreOpenedWindows
    {
        get
        {
            var windows = FindObjectOfType<WindowsManager>().windows; // Najdi všechna okna
            foreach (var item in windows)
            {
                // Pokud je některé okno otevřené, vrať true
                if (item.IsOpened) return true;
            }
            return false; // Pokud žádné okno není otevřené, vrať false
        }
    }
}

[Serializable]
public class Window
{
    [SerializeField] GameObject Window_; // Odkaz na herní objekt okna
    [SerializeField] KeyCode Key; // Klávesa pro ovládání okna

    // Zapni okno
    public void TurnOn()
    {
        if (Window_.GetComponent<Animator>())
        {
            var anim = Window_.GetComponent<Animator>();
            anim.SetBool("Opened", true);
        }
        else
        {
            Window_.SetActive(true);
        }
    }

    // Vypni okno
    public void TurnOff()
    {
        if (Window_.GetComponent<Animator>())
        {
            var anim = Window_.GetComponent<Animator>();
            anim.SetBool("Opened", false);
        }
        else
        {
            Window_.SetActive(false);
        }
    }

    // Přepni stav okna (otevřeno/uzavřeno)
    public void Switch()
    {
        if (IsOpened)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    // Vlastnost pro kontrolu, zda je okno otevřené
    public bool IsOpened
    {
        get {
            if (Window_.GetComponent<Animator>())
            {
                var anim = Window_.GetComponent<Animator>();
                return anim.GetBool("Opened");
            }
            else
            {
                return Window_.activeSelf;
            }
        } // Vrátí true, pokud je okno aktivní
    }

    // Vlastnost pro přístup k přiřazené klávese
    public KeyCode KeyCode { get { return Key; } }
}
