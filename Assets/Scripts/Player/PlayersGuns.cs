using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersGuns : MonoBehaviour
{
    // Start is called before the first frame update
    ItemsData itemsData;
    PlayerInventory PI;
    [SerializeField] List<GunData> guns;
    void Start()
    {
        itemsData = FindObjectOfType<ItemsData>();
        PI = GetComponent<PlayerInventory>();
        SetGunByInventory();
    }
    public void SetGunByInventory()
    {
        if(PI.GunCell.MyID == -1)
        {
            foreach (var gun in guns)
            {
                SetGun(gun, false);
            }
            return;
        }
        foreach (var gun in guns)
        {
            SetGun(gun, false);
        }
        SetGun(guns.Find(x => x.ItemID == PI.GunCell.MyID), true);
    }
    void SetGun(GunData gd, bool activated)
    {
        gd.GunObject.SetActive(activated);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class GunData
{
    [SerializeField] GameObject gunObject;
    [SerializeField] int itemID;
    public int ItemID => itemID;
    public GameObject GunObject => gunObject;
}