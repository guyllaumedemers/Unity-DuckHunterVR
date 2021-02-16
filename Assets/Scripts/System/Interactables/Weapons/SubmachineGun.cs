using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : BaseWeapon
{
    [field: SerializeField] public float RateOfFire { get; set; } = 0f;
    public float TimeBeforeNextShot { get; set; } = 0f;

    public override void Shoot()
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