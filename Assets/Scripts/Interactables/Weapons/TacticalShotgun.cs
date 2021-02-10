using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalShotgun : BaseWeapon
{
    void Start()
    {
        GunTip = GameObject.Find("TacticalShotgunGunTip");
    }
}
