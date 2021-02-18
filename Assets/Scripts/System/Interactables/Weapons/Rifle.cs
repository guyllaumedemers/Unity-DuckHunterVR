using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : SingleShotAmmo
{
    protected override string ShootingSoundPath => "SFX/Guns/ActionRifle";
    protected override string ReloadingSoundPath => "SFX/Guns/RifleReload";
}
