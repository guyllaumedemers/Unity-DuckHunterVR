using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Pistol : BaseWeapon
{
    private void Awake()
    {
        ShootingSound = Resources.Load<AudioClip>("SFX/Guns/45Gunshot");
        ReloadSound = Resources.Load<AudioClip>("SFX/Guns/MagazineReload");
    }

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