using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model700 : BaseWeapon
{
    void Start()
    {
        GunTip = GameObject.Find("Model700GunTip");
    }
}
