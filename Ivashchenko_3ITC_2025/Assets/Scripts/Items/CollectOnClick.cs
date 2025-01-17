using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnClick : MonoBehaviour
{
    [SerializeField] int id;             // Identifikátor položky (item ID)
    [SerializeField] int count;          // Počet položek k vyzvednutí
    const int distanceToTake = 4;        // Maximální vzdálenost pro vyzvednutí
    PlayerInventory inventory;

    // Vlastnosti pro ID a počet položek
    public int ID => id;
    public int Count => count;

    // Inicializace komponenty - vyhledání inventáře hráče
    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }

    // Detekuje kliknutí myší na objekt
    private void OnMouseDown()
    {
        // Kontroluje, zda je hráč dostatečně blízko pro vyzvednutí položky
        if (Vector3.Distance(transform.position, inventory.transform.position) < distanceToTake)
        {
            // Přidá položku do inventáře a smaže objekt, pokud byl vyzvednut
            var wasTaken = inventory.PutToInventory(ID, count);
            if (wasTaken) Destroy(gameObject);
        }
    }
}
