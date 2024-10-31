using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneLoader2 : MonoBehaviour
{
    Button button; // Odkaz na komponentu tlačítka
    [SerializeField] string SceneName; // Název scény, kterou chcete načíst

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); // Získá komponentu Button připojenou k tomuto GameObject
        button.onClick.RemoveAllListeners(); // Odstraní všechny dřívější posluchače událostí kliknutí
        // Přidá posluchače, který načte scénu, když je tlačítko stisknuto
        button.onClick.AddListener(delegate
        {
            LoadScene.LoadSceneGlobally(SceneName); // Načte scénu globálně na základě zadaného názvu
        });
    }
}
