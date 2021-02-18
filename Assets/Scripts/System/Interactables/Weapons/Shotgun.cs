using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : SingleShotAmmo
{
    private void Awake()
    {
        ShootingSound = Resources.Load<AudioClip>("SFX/Guns/Shotgun");
        ReloadSound = Resources.Load<AudioClip>("SFX/Guns/ShotgunReload");
    }
}
