using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<InventoryCell> inventoryCells;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cell in inventoryCells)
        {
            cell.ResetCell();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
