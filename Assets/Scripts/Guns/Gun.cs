using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float shootRate;
    [SerializeField] protected Transform shotOrigin;
    [SerializeField] protected Transform shotDir;
    protected float shotPause;
    protected float timeSinceLastShot;
    protected virtual void Start()
    {
        shotPause = 1f / shootRate;
        timeSinceLastShot = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButton(0) && !WindowsManager.AreOpenedWindows && timeSinceLastShot > shotPause)
        {
            Shoot();
        }
        animator.SetBool("Center", Input.GetMouseButton(1));
    }
    protected virtual void Shoot()
    {
        Debug.Log("Shot!");
        timeSinceLastShot = 0;
        animator.SetTrigger("Shot");
        RaycastHit hit;
        if(Physics.Raycast(new Ray(shotOrigin.position, shotDir.position), out hit))
        {
            Debug.Log($"{hit.collider.name} was hit");
        }
    }
}
