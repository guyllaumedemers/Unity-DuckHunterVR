using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoContainer : MonoBehaviour, IAmmoContainer
{
    public Rigidbody Rigidbody { get; set; }
    public BoxCollider BoxCollider { get; set; }
    [field: SerializeField] public int CurrentAmmo { get; set; }
    [field: SerializeField] public bool CanLoad { get; set; }

    void Start()
    {
        CanLoad = true;
        Rigidbody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            tag = "PistolMagazine";
        }
    }
}
