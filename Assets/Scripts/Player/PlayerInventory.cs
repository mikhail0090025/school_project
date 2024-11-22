using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<InventoryCell> inventoryCells; // Seznam buněk inventáře
    [SerializeField] InventoryCell gunCell; // Buněčný prostor pro zbraň
    public InventoryCell GunCell => gunCell; // Vlastnost pro přístup k buněčnému prostoru pro zbraň
    ItemsData ItemsData; // Odkaz na data o položkách

    // Start is called before the first frame update
    void Start()
    {
        ItemsData = FindObjectOfType<ItemsData>(); // Najdi data o položkách
        foreach (var cell in inventoryCells)
        {
            cell.ResetCell(); // Resetuj každou buňku
        }
        gunCell.SetCell(1, 0); // Nastav zbraň na pozici 0 s množstvím 1
        //PutToInventory(1, 1); // (Komentováno) Přidání položky do inventáře
        RefreshInventory(); // Obnov inventář
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Metoda pro počítání položek v inventáři podle ID
    public int CountInInventory(int id)
    {
        int res = 0; // Výsledný počet
        if (GunCell.MyID == id) res++; // Pokud je zbraň v inventáři, zvyšte počet
        foreach (var cell in inventoryCells)
        {
            if (cell.MyID == id) res += cell.Count; // Přidejte k počtu podle počtu v buňce
        }
        return res; // Vraťte celkový počet
    }

    // Obnoví stav inventáře
    public void RefreshInventory()
    {
        gunCell.Refresh(); // Obnov zbraň
        foreach (var item in inventoryCells)
        {
            item.Refresh(); // Obnov každou buňku
        }
    }

    // Přidání položky do inventáře
    public bool PutToInventory(int ID, int count)
    {
        // Zkontrolujte, zda položka může být přidána do inventáře
        if (ItemsData.GetByID_(ID).OnlyOne && CountInInventory(ID) > 0)
        {
            return false; // Pokud je položka jedinečná a již je v inventáři, návrat false
        }

        var cell = CellWithItem(ID); // Získejte buňku s touto položkou
        if (cell == null)
            cell = EmptyCell(); // Pokud není buňka, získat prázdnou buňku
        if (cell == null) // Pokud nebyla nalezena žádná buňka
        {
            Debug.LogException(new System.Exception("There are not cells to put")); // Vytvoření výjimky
            return false;
        }
        else
        {
            cell.SetCell(cell.Count + count, ID); // Nastavte buňku s množstvím a ID
        }
        RefreshInventory(); // Obnovte inventář
        return true; // Návrat true, pokud byla položka úspěšně přidána
    }

    // Získejte buňku s danou položkou podle ID
    public InventoryCell CellWithItem(int ID)
    {
        if (inventoryCells.Find(x => x.MyID == ID))
            return inventoryCells.Find(x => x.MyID == ID) as InventoryCell; // Vrátí buňku
        else
        {
            return null; // Pokud není nalezena, vraťte null
        }
    }

    // Získejte prázdnou buňku
    public InventoryCell EmptyCell()
    {
        if (inventoryCells.Find(x => x.MyID == -1))
            return inventoryCells.Find(x => x.MyID == -1) as InventoryCell; // Vrátí prázdnou buňku
        else
        {
            return null; // Pokud není nalezena, vraťte null
        }
    }

    // Zpracování kliknutí na buňku
    public void CellClicked(InventoryCell cell)
    {
        if (cell == gunCell) // Pokud klikneme na buňku zbraně
        {
            var gun_id = GunCell.MyID; // Získejte ID zbraně
            GunCell.ResetCell(); // Resetuj buňku zbraně
            PutToInventory(gun_id, 1); // Přidejte zbraň zpět do inventáře
            GetComponent<PlayersGuns>().SetGunByInventory(); // Nastavte zbraň podle inventáře
            return;
        }
        else
        {
            for (global::System.Int32 i = 0; i < inventoryCells.Count; i++)
            {
                if (cell == inventoryCells[i]) // Pokud kliknutá buňka je v inventáři
                {
                    var item = this.ItemsData.Items.Find(x => x.ID == inventoryCells[i].MyID); // Najdi položku podle ID
                    if ((item.Category & Category.Gun) == Category.Gun && GunCell.IsEmpty) // Pokud je položka zbraň a prostor pro zbraň je prázdný
                    {
                        GunCell.SetCell(1, inventoryCells[i].MyID); // Nastavte zbraň do buňky
                        cell.SubstractItem(1); // Odeberte jednu položku z kliknuté buňky
                        GetComponent<PlayersGuns>().SetGunByInventory(); // Nastavte zbraň podle inventáře
                    }
                    return;
                }
            }
        }
        Debug.LogError("Clicked cell was not defined!"); // Chybová zpráva, pokud buňka nebyla nalezena
        RefreshInventory(); // Obnovte inventář
    }
}
