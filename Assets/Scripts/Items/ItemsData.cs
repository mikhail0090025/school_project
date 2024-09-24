using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public List<ItemData> Items;
    static ItemsData instance;
    public ItemData GetByID(int id)
    {
        return Items.Find(x => x.ID == id);
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = FindObjectOfType<ItemsData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static ItemData GetByID_(int id) => instance.GetByID(id);
}
[Serializable]
public class ItemData
{
    [SerializeField] Sprite texture;
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Category category;
    [SerializeField] bool onlyOne; // Player can have only one object of this type in inventory
    public Sprite Texture => texture;
    public int ID => id;
    public string Name => name;
    public string Description => description;
    public Category Category => category;
    public bool OnlyOne => onlyOne;

}
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