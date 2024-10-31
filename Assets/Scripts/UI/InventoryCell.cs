using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] Button CellButton;               // Tlačítko buňky (cell)
    [SerializeField] TMPro.TMP_Text CellText;         // Text pro zobrazení počtu položek
    [SerializeField] Image CellImage;                 // Obrázek položky v inventáři
    [SerializeField] int ID;                          // ID položky v buňce
    [SerializeField] int count;                       // Počet položek v buňce

    public bool IsEmpty => count <= 0 || ID < 0;      // Vrací true, pokud je buňka prázdná
    public int MyID => ID;                            // Vrátí ID položky v buňce
    public void SetID(int ID) { this.ID = ID; }       // Nastaví ID položky
    public int Count => count;                        // Vrátí počet položek
    public void SetCount(int count_) { this.count = count_; } // Nastaví počet položek
    PlayerInventory PI;                               // Odkaz na inventář hráče

    void Start()
    {
        PI = FindObjectOfType<PlayerInventory>();     // Najde instanci inventáře hráče
        CellButton.onClick.RemoveAllListeners();      // Odstraní všechny posluchače z tlačítka
        CellButton.onClick.AddListener(delegate       // Přidá posluchač pro kliknutí na buňku
        {
            PI.CellClicked(this);
        });
        Refresh();                                    // Aktualizuje zobrazení buňky
    }

    // Aktualizuje text a obrázek buňky na základě jejího stavu
    public void Refresh()
    {
        if (ID == -1)
        {
            CellText.text = $"n";                     // Zobrazuje "n", pokud je ID -1 (prázdná buňka)
            CellImage.sprite = null;                  // Vymaže obrázek buňky
        }
        else
        {
            CellText.text = $"x{count}";              // Zobrazuje počet položek
            var item = FindObjectOfType<ItemsData>().Items.Find(x => x.ID == ID); // Najde položku podle ID
            CellImage.sprite = item.Texture;          // Nastaví obrázek položky
        }
    }

    // Odečte položku z buňky
    public void SubstractItem(int count)
    {
        Debug.Log($"Subtraction was called! {count}");
        if (ID == -1 || this.count < count)            // Pokud je buňka prázdná nebo počet je menší než požadovaný
        {
            Debug.LogError("Subtraction is impossible");
        }
        else
        {
            this.count -= count;                      // Odečte požadovaný počet položek
            Debug.Log($"Subtraction result: {this.count}");
            if (this.count <= 0) this.ResetCell();    // Pokud je počet položek 0 nebo méně, resetuje buňku
        }
    }

    // Resetuje buňku do výchozího stavu (prázdná)
    public void ResetCell()
    {
        ID = -1;
        count = 0;
        Refresh();
    }

    // Nastaví buňku s novým počtem a ID položky
    public void SetCell(int count, int id)
    {
        ID = id;
        this.count = count;
        Refresh();
    }

    // Update metoda, volána každou snímku (zatím prázdná)
    void Update()
    {

    }
}
