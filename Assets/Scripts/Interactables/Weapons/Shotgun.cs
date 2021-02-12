using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    private void Awake()
    {
        IsAcceptingMagazine = false;
    }
}
