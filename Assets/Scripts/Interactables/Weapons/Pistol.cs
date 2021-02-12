using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Pistol : BaseWeapon
{
    private void Awake()
    {
        IsAcceptingMagazine = true;
    }
}
