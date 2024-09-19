using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public List<ItemData> Items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class ItemData
{
    [SerializeField] Sprite texture;
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Category category;
    public Sprite Texture
    {
        get { return texture; }
    }
    public int ID
    {
        get { return id; }
    }
    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public Category Category
    {
        get { return category; }
    }

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