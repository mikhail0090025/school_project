using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RefreshAmmoUI))]
public class Gun : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float shootRate;
    [SerializeField] protected Transform shotOrigin;
    [SerializeField] protected Transform shotDir;
    [SerializeField] protected int MaxAmmo;
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject bullet;
    protected float rechargeTime;
    protected float shotPause;
    protected int currentAmmo;
    protected float timeSinceLastShot;
    RefreshAmmoUI m_RefreshAmmoUI;
    public int CurrentAmmo => currentAmmo;
    public int Max_Ammo => MaxAmmo;
    protected bool Recharging;
    public bool IsRecharging => Recharging;
    public float RechargeTimeNeeded;

    // Inicializace nastavení zbraně při startu
    protected virtual void Start()
    {
        shotPause = 1f / shootRate;
        timeSinceLastShot = 0f;
        currentAmmo = MaxAmmo;
        m_RefreshAmmoUI = GetComponent<RefreshAmmoUI>();
        m_RefreshAmmoUI.RefreshUI(this);
    }

    // Aktualizace se volá každý snímek
    protected virtual void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Pokud zbraň dobíjí, zvyšuje čas pro nabíjení
        if (Recharging)
        {
            rechargeTime += Time.deltaTime;
            if (rechargeTime >= RechargeTimeNeeded)
            {
                // Dokončení dobíjení
                Recharging = false;
                rechargeTime = 0f;
                currentAmmo = MaxAmmo; // Doplnění munice
            }
            return;
        }

        // Kontrola, zda lze vystřelit
        if (Input.GetMouseButton(0) &&
            !WindowsManager.AreOpenedWindows &&
            timeSinceLastShot > shotPause &&
            currentAmmo > 0)
        {
            Shoot();
        }
        else if (currentAmmo <= 0 && !Recharging)
        {
            Recharge(); // Zahájení dobíjení, pokud není munice
        }

        // Ruční dobíjení při stisknutí klávesy R
        if (Input.GetKey(KeyCode.R))
        {
            Recharge();
        }

        animator.SetBool("Center", Input.GetMouseButton(1));
        m_RefreshAmmoUI.RefreshUI(this);
    }

    // Funkce pro střelbu
    protected virtual void Shoot()
    {
        timeSinceLastShot = 0;
        animator.SetTrigger("Shot");
        RaycastHit hit;

        Vector3 shootDirection = shotOrigin.forward;

        if (Physics.Raycast(new Ray(shotOrigin.position, shootDirection), out hit))
        {
            Debug.DrawRay(transform.position, shootDirection, Color.blue, 1f);
            Debug.Log($"{hit.collider.name} was hit");

            if (hit.collider.gameObject.GetComponent<HPscript>())
            {
                hit.collider.gameObject.GetComponent<HPscript>().Damage(damage * (Random.Range(50, 150) / 100f));
            }
        }

        currentAmmo--;
    }


    // Funkce pro zahájení dobíjení
    protected virtual void Recharge()
    {
        if (Recharging || currentAmmo == MaxAmmo) return; // Zastaví dobíjení, pokud je již plné nebo probíhá
        Recharging = true;
        rechargeTime = 0f; // Reset časovače dobíjení
    }
}
