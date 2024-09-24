using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] Button CellButton;
    [SerializeField] TMPro.TMP_Text CellText;
    [SerializeField] Image CellImage;
    [SerializeField] int ID;
    [SerializeField] int count;
    public bool IsEmpty => count <= 0 || ID < 0;
    public int MyID => ID;
    public void SetID(int ID) {  this.ID = ID; }
    public int Count => count;
    public void SetCount(int count_) { this.count = count_; }
    PlayerInventory PI;
    void Start()
    {
        PI = FindObjectOfType<PlayerInventory>();
        CellButton.onClick.RemoveAllListeners();
        CellButton.onClick.AddListener(delegate
        {
            PI.CellClicked(this);
        });
        Refresh();
    }
    public void Refresh()
    {
        if(ID == -1)
        {
            CellText.text = $"n";
            CellImage.sprite = null;
        }
        else
        {
            CellText.text = $"x{count}";
            var item = FindObjectOfType<ItemsData>().Items.Find(x => x.ID == ID);
            CellImage.sprite = item.Texture;
        }
    }
    public void SubstractItem(int count)
    {
        Debug.Log($"Substraction was called! {count}");
        if(ID == -1 || this.count < count)
        {
            Debug.LogError("Substraction is impossible");
        }
        else
        {
            this.count -= count;
            Debug.Log($"Substraction result: {this.count}");
            if (this.count <= 0) this.ResetCell();
        }
    }
    public void ResetCell()
    {
        ID = -1;
        count = 0;
        Refresh();
    }
    public void SetCell(int count, int id)
    {
        ID = id;
        this.count = count;
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}