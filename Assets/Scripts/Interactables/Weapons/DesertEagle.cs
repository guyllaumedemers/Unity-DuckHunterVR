using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DesertEagle : BaseWeapon
{
    void Awake()
    {
        GunTip = GameObject.Find("DesertEagleGunTip");
    }
}
