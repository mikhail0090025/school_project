using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneLoader : MonoBehaviour
{
    Button button; // Odkaz na komponentu tlačítka
    [SerializeField] string SceneName; // Název scény, kterou chceme načíst

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); // Získá komponentu Button připojenou k tomuto GameObject
        button.onClick.RemoveAllListeners(); // Odstraní všechny dřívější posluchače událostí kliknutí
        // Přidá posluchače, který načte novou scénu, když je tlačítko stisknuto
        button.onClick.AddListener(delegate
        {
            SceneManager.LoadScene(SceneName); // Načte scénu podle názvu
        });
    }
}
