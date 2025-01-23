using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersGuns : MonoBehaviour
{
    ItemsData itemsData; // Odkaz na data o položkách
    PlayerInventory PI; // Odkaz na inventář hráče
    [SerializeField] List<GunData> guns; // Seznam dat o zbraních

    // Start is called before the first frame update
    void Start()
    {
        itemsData = FindObjectOfType<ItemsData>(); // Najdi data o položkách
        PI = GetComponent<PlayerInventory>(); // Získejte komponentu inventáře hráče
        SetGunByInventory(); // Nastavte zbraň podle inventáře
    }

    // Nastavení zbraně podle inventáře
    public void SetGunByInventory()
    {
        if (PI.GunCell.MyID == -1) // Pokud není zbraň přiřazena
        {
            foreach (var gun in guns)
            {
                SetGun(gun, false); // Deaktivuj všechny zbraně
            }
            return; // Ukonči metodu
        }

        foreach (var gun in guns)
        {
            SetGun(gun, false); // Deaktivuj všechny zbraně
        }

        // Aktivuj zbraň podle ID v inventáři
        SetGun(guns.Find(x => x.ItemID == PI.GunCell.MyID), true);
    }

    // Nastavení stavu zbraně (aktivováno/deaktivováno)
    void SetGun(GunData gd, bool activated)
    {
        gd.GunObject.SetActive(activated); // Aktivuj nebo deaktivuj zbraň
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class GunData
{
    [SerializeField] GameObject gunObject; // Odkaz na herní objekt zbraně
    [SerializeField] int itemID; // ID položky zbraně
    public int ItemID => itemID; // Vlastnost pro přístup k ID položky
    public GameObject GunObject => gunObject; // Vlastnost pro přístup k hernímu objektu zbraně
}
