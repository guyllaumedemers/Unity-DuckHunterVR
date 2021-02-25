using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateRefill : MonoBehaviour
{
    private Transform ammoParent;
    private int nbChildren;
    public AmmoContainer ammoPrefab;
    public int qtyAmmoSpawn;
    public float TimeToRefill = 3f;
    private float TimeBeforeRefill;
    private bool _isRefilled = false;
    private bool _isWaitingForRefill = true;

    private void Start()
    {
        ammoParent = transform.GetChild(0);
    }

    private void Update()
    {
        nbChildren = ammoParent.transform.childCount;

        if (nbChildren == 0 && _isRefilled)
        {
            _isRefilled = false;
            _isWaitingForRefill = true;
        }

        if (nbChildren == 0 && _isWaitingForRefill && !_isRefilled)
        {
            TimeBeforeRefill = Time.time + TimeToRefill;
            _isWaitingForRefill = false;
        }

        if (Time.time >= TimeBeforeRefill && !_isRefilled)
            RefillCrate();
    }

    private void RefillCrate()
    {
        for (int i = 0; i < qtyAmmoSpawn; i++)
        {
            AmmoContainer magazineClone = Instantiate(ammoPrefab, transform.position, transform.rotation, ammoParent.transform);
            magazineClone.name = ammoPrefab.name;
            magazineClone.transform.SetParent(ammoParent.transform);
        }

        _isRefilled = true;
    }
}
