using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] Button CellButton;
    [SerializeField] TMPro.TMP_Text CellText;
    [SerializeField] Image CellImage;
    [SerializeField] int ID;
    [SerializeField] int count;
    void Start()
    {

    }
    public void Refresh()
    {
        CellText.text = $"x{count}";
        FindObjectOfType<ItemsData>().Items.Find(x => x.ID == ID);
    }
    public void ResetCell()
    {
        ID = -1;
        count = 0;
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}