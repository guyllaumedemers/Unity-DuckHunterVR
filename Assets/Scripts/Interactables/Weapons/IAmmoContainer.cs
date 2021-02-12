using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmoContainer
{
    public int CurrentAmmo { get; set; }
    public int MaxAmmo { get; set; }
}
