using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BaseWeapon
{
    private void Start()
    {
        GunTip = GameObject.Find("Model700GunTip");
        AmmoReloadCollider = GameObject.FindGameObjectWithTag("RifleReload").GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Contains("RifleAmmo") && AmmoReloadCollider.tag == "RifleReload")
        {
            CurrentAmmo = MaxAmmo;
        }
    }
}
