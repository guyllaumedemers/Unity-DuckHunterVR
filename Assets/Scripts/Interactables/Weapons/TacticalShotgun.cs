using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalShotgun : BaseWeapon
{
    private void Start()
    {
        GunTip = GameObject.Find("TacticalShotgunGunTip");
        AmmoReloadCollider = GameObject.FindGameObjectWithTag("ShotgunReload").GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Contains("ShotgunAmmo") && AmmoReloadCollider.tag == "ShotgunReload")
        {
            CurrentAmmo = MaxAmmo;
        }
    }
}
