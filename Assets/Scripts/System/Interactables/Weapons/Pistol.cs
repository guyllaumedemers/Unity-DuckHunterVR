using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Pistol : BaseWeapon
{
    protected override string ShootingSoundPath => "SFX/Guns/45Gunshot";
    protected override string ReloadingSoundPath => "SFX/Guns/MagazineReload";

    public override void Shoot()
    {
        if (CurrentMagazine != null)
        {
            if (CurrentAmmoContainer.CurrentAmmo > 0)
            {
                base.Shoot();
                CurrentAmmoContainer.CurrentAmmo--;
            }
        }
    }
}