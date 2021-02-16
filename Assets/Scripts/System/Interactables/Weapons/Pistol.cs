using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Pistol : BaseWeapon
{
    public override void Shoot()
    {
        if (CurrentAmmoContainer.CurrentAmmo > 0)
        {
            base.Shoot();
            CurrentAmmoContainer.CurrentAmmo--;
        }
    }
}