using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<InventoryCell> inventoryCells;
    [SerializeField] InventoryCell gunCell;
    public InventoryCell GunCell => gunCell;
    ItemsData ItemsData;
    // Start is called before the first frame update
    void Start()
    {
        ItemsData = FindObjectOfType<ItemsData>();
        foreach (var cell in inventoryCells)
        {
            cell.ResetCell();
        }
        gunCell.SetCell(1, 0);
        //PutToInventory(1, 1);
        RefreshInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int CountInInventory(int id)
    {
        int res = 0;
        if (GunCell.MyID == id) res++;
        foreach (var cell in inventoryCells)
        {
            if (cell.MyID == id) res += cell.Count;
        }
        return res;
    }
    public void RefreshInventory()
    {
        gunCell.Refresh();
        foreach (var item in inventoryCells)
        {
            item.Refresh();
        }
    }
    public bool PutToInventory(int ID, int count)
    {
        if(ItemsData.GetByID_(ID).OnlyOne && CountInInventory(ID) > 0)
        {
            return false;
        }
        var cell = CellWithItem(ID);
        if (cell == null) cell = EmptyCell();
        else
        {
            cell.SetCount(cell.Count + count);
        }
        if (cell == null)
        {
            Debug.LogException(new System.Exception("There are not cells to put"));
            return false;
        }
        else
        {
            cell.SetCell(count, ID);
        }
        RefreshInventory();
        return true;
    }
    public InventoryCell CellWithItem(int ID)
    {
        if (inventoryCells.Find(x => x.MyID == ID)) return inventoryCells.Find(x => x.MyID == ID) as InventoryCell;
        else
        {
            return null;
        }
    }
    public InventoryCell EmptyCell()
    {
        if (inventoryCells.Find(x => x.MyID == -1)) return inventoryCells.Find(x => x.MyID == -1) as InventoryCell;
        else {
            return null;
        }
    }
    public void CellClicked(InventoryCell cell)
    {
        if(cell == gunCell)
        {
            var gun_id = GunCell.MyID;
            GunCell.ResetCell();
            PutToInventory(gun_id, 1);
            GetComponent<PlayersGuns>().SetGunByInventory();
            return;
        }
        else
        {
            for (global::System.Int32 i = 0; i < inventoryCells.Count; i++)
            {
                if(cell == inventoryCells[i])
                {
                    var item = this.ItemsData.Items.Find(x => x.ID == inventoryCells[i].MyID);
                    if ((item.Category & Category.Gun) == Category.Gun && GunCell.IsEmpty)
                    {
                        GunCell.SetCell(1, inventoryCells[i].MyID);
                        cell.SubstractItem(1);
                        GetComponent<PlayersGuns>().SetGunByInventory();
                    }
                    return;
                }
            }
        }
        Debug.LogError("Clicked cell was not defined!");
        RefreshInventory();
    }
}
