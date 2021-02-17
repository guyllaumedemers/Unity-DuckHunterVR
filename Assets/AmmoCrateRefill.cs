using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateRefill : MonoBehaviour
{
    private Transform ammoParent;
    private int nbChildren;
    public GameObject ammoPrefab;    
    public int qtyAmmoSpawn;    

    private void Start()
    {
        ammoParent = transform.GetChild(0);
    }

    private void Update()
    {
        if(nbChildren == 0)
        {
            for (int i = 0; i < qtyAmmoSpawn; i++)
            {
                GameObject magazineClone = Instantiate(ammoPrefab, transform.position, transform.rotation, ammoParent.transform);
                magazineClone.transform.SetParent(ammoParent.transform);
            }
        }

        nbChildren = ammoParent.transform.childCount;
    }
}
