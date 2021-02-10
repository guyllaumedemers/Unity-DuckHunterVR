using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : BaseWeapon
{
    void Start()
    {
        GunTip = GameObject.Find("SubmachineGunGunTip");
    }
}
