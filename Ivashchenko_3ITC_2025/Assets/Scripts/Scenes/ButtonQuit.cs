using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonQuit : MonoBehaviour
{
    Button button; // Odkaz na komponentu tlačítka

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); // Získá komponentu Button připojenou k tomuto GameObject
        button.onClick.RemoveAllListeners(); // Odstraní všechny dřívější posluchače událostí kliknutí
        // Přidá posluchače, který ukončí aplikaci, když je tlačítko stisknuto
        button.onClick.AddListener(delegate
        {
            Application.Quit(); // Ukončí aplikaci
        });
    }
}
