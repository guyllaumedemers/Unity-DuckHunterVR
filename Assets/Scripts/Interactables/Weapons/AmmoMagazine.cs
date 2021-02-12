using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMagazine : MonoBehaviour, IAmmoMagazine
{
    [field: SerializeField] public BoxCollider ClipCollider { get; set; }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Floor"))
        {
            tag = "PistolMagazine";
        }
    }    
}
