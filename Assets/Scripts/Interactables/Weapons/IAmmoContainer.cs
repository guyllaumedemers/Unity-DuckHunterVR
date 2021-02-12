using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmoContainer
{
    public Rigidbody Rigidbody { get; set; }
    public BoxCollider BoxCollider { get; set; }
    public int CurrentAmmo { get; set; }
    public bool CanLoad { get; set; }
}
