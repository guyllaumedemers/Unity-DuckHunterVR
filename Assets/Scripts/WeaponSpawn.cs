using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject weaponSpawnPrefab;
    private GameObject weaponSpawnClone;

    private void Awake()
    {
        WeaponResetButton.Instance.AddSpawnerToList(this);
    }

    public void InstanciateWeapon()
    {
        weaponSpawnClone = Instantiate(weaponSpawnPrefab, transform.position, transform.rotation, transform);
        weaponSpawnClone.name = weaponSpawnPrefab.name;
    }

    public void DestroyWeapon()
    {
        Destroy(weaponSpawnClone.gameObject);
    }
}
