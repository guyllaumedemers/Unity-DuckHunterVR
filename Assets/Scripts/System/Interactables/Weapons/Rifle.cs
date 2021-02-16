using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : SingleShotAmmo
{
    private void Awake()
    {
        ShootingSound = Resources.Load<AudioClip>("SFX/Guns/ActionRifle");
        ReloadSound = Resources.Load<AudioClip>("SFX/Guns/RifleReload");
    }
}
