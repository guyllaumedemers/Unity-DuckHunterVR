using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClip : MonoBehaviour, IAmmoClip
{
    [field: SerializeField] public BoxCollider ClipCollider { get; set; }
    Collider testColl;

    // Start is called before the first frame update
    void Start()
    {
        //ClipCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.name);

        /*if (collider.name.Contains("Target"))
        {
            ClipCollider.isTrigger = false;
        }*/
    }

    
}
