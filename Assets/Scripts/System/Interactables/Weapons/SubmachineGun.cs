using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : BaseWeapon
{
    protected override string ShootingSoundPath => "SFX/Guns/AR15";
    protected override string ReloadingSoundPath => "SFX/Guns/MagazineReload";

    [field: Header("Automatic Weapon Options")]
    [field: SerializeField] public float RateOfFire { get; set; } = 0f;
    public float TimeBeforeNextShot { get; set; } = 0f;

    public override void Shoot()
    {
        if(CurrentMagazine != null)
        {
            if (Time.time >= TimeBeforeNextShot)
            {
                if (CurrentAmmoContainer.CurrentAmmo > 0)
                {
                    base.Shoot();
                    CurrentAmmoContainer.CurrentAmmo--;
                }

                TimeBeforeNextShot = Time.time + RateOfFire;
            }
        }
    }
}