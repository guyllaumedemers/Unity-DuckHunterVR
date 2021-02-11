using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BaseWeapon
{
    void Start()
    {
        GunTip = GameObject.Find("Model700GunTip");
    }
}
