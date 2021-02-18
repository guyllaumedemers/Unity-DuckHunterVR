using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IAmmoContainer
{
    public Rigidbody Rigidbody { get; set; }
    public BoxCollider BoxCollider { get; set; }
    public int CurrentAmmo { get; set; }
    public bool MagazineCanLoad { get; set; }
    public bool IsMagazine { get; set; }
    public float TimeCanLoad { get; set; }
    public float TimeBeforeCanLoad { get; set; }
    public XRGrabInteractable GrabInteractable { get; set; }
    public float DestroyDistance { get; set; }
}
