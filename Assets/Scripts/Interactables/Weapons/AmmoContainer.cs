using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoContainer : MonoBehaviour, IAmmoContainer
{
    [field: SerializeField] public int CurrentAmmo { get; set; }
    [field: SerializeField] public int MaxAmmo { get; set; }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Floor"))
        {
            tag = "PistolMagazine";
        }
    }    
}
