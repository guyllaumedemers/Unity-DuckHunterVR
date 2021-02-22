using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject weaponSpawnPrefab;

    private void Awake()
    {
        GameObject weaponSpawnClone = Instantiate(weaponSpawnPrefab, transform.position, Quaternion.identity, transform);
        weaponSpawnClone.transform.name = weaponSpawnClone.name.Replace("(clone)", "").Trim();
    }
}
