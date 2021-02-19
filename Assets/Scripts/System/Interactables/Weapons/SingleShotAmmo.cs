using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotAmmo : BaseWeapon
{
    [field: SerializeField] public int CurrentAmmo { get; set; } = 0;
    [field: SerializeField] public int MaxAmmo { get; set; } = 10;

    public override void Shoot()
    {
        if (CurrentAmmo > 0)
        {
            base.Shoot();
            CurrentAmmo--;
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(name + "AmmoBox") && AmmoReloadCollider.CompareTag(name + "Reload"))
        {
            CurrentAmmoContainer = collider.GetComponent<AmmoContainer>();

            int ammoNeeded = MaxAmmo - CurrentAmmo;

            if (ammoNeeded > 0 && CurrentAmmoContainer.CurrentAmmo > 0)
            {
                if (AudioSource.clip != ReloadSound)
                    AudioSource.clip = ReloadSound;

                AudioSource.Play();
            }

            for (int i = 0; i < ammoNeeded; i++)
            {
                if (CurrentAmmoContainer.CurrentAmmo != 0)
                {
                    CurrentAmmo++;
                    CurrentAmmoContainer.CurrentAmmo--;
                }
            }

            CurrentAmmoContainer = null;
        }
    }
}
