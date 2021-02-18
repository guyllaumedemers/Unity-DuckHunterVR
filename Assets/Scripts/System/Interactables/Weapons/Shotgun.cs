using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : SingleShotAmmo
{
    protected override string ShootingSoundPath => "SFX/Guns/Shotgun";
    protected override string ReloadingSoundPath => "SFX/Guns/ShotgunReload";
}
