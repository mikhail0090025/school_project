using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public List<ItemData> Items; // Seznam položek (items) typu ItemData
    static ItemsData instance; // Statická instance pro usnadnění přístupu

    // Vrátí položku podle jejího ID
    public ItemData GetByID(int id)
    {
        return Items.Find(x => x.ID == id);
    }

    // Inicializace instance při startu
    void Start()
    {
        instance = FindObjectOfType<ItemsData>();
    }

    // Statická metoda pro získání položky podle ID
    public static ItemData GetByID_(int id) => instance.GetByID(id);
}

[Serializable]
public class ItemData
{
    [SerializeField] Sprite texture;           // Textura položky (ikona, obrázek)
    [SerializeField] int id;                   // Unikátní ID položky
    [SerializeField] string name;              // Jméno položky
    [SerializeField] string description;       // Popis položky
    [SerializeField] Category category;        // Kategorie položky
    [SerializeField] bool onlyOne;             // Určuje, zda hráč může mít pouze jednu položku tohoto typu
    [SerializeField] GameObject droppedObject;    // Dropped

    // Vlastnosti pro přístup k datům položky
    public Sprite Texture => texture;
    public int ID => id;
    public string Name => name;
    public string Description => description;
    public Category Category => category;
    public bool OnlyOne => onlyOne;
    public GameObject DroppedObject => droppedObject;
}

// Výčtový typ (enum) s atributem [Flags] pro kategorizaci položek
[Flags]
public enum Category
{
    None = 0,
    Gun = 1 << 0,
    Nature = 1 << 1,
    Food = 1 << 2,
    Medical = 1 << 3,
    Armor = 1 << 4
};
