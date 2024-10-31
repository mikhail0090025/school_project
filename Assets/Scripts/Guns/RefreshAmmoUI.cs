using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefreshAmmoUI : MonoBehaviour
{
    [SerializeField] TMP_Text AmmoText;           // Textový prvek pro zobrazení počtu nábojů
    [SerializeField] TMP_Text RechargeText;       // Textový prvek pro zobrazení stavu nabíjení

    // Aktualizuje UI na základě stavu aktuální zbraně
    public void RefreshUI(Gun currGun)
    {
        AmmoText.text = $"{currGun.CurrentAmmo} / {currGun.Max_Ammo}";   // Nastaví text nábojů
        RechargeText.gameObject.SetActive(currGun.IsRecharging);          // Zobrazí text nabíjení, pokud je aktivní
    }
}
