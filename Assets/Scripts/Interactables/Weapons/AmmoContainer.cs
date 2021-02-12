using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AmmoContainer : MonoBehaviour, IAmmoContainer
{
    public Rigidbody Rigidbody { get; set; }
    public BoxCollider BoxCollider { get; set; }
    [field: SerializeField] public int CurrentAmmo { get; set; }
    public bool MagazineCanLoad { get; set; }
    [field: SerializeField] public bool IsMagazine { get; set; }
    public float TimeCanLoad { get; set; } = 0.5f;
    public float TimeBeforeCanLoad { get; set; } = 0f;
    public XRGrabInteractable GrabInteractable { get; set; }

    void Start()
    {
        MagazineCanLoad = true;
        Rigidbody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        GrabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (!MagazineCanLoad && CurrentAmmo > 0)
        {
            if (Time.time >= TimeBeforeCanLoad)
            {
                MagazineCanLoad = true;
            }
        }
        else if (!MagazineCanLoad && CurrentAmmo == 0 || !IsMagazine && CurrentAmmo == 0)
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
