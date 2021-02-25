using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponResetButton : MonoBehaviour
{
    #region Singleton
    private static WeaponResetButton instance;
    private WeaponResetButton() { }
    public static WeaponResetButton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WeaponResetButton();
            }
            return instance;
        }
    }
    #endregion
    private new Animation animation;

    public List<WeaponSpawn> weaponSpawns;

    private void Awake()
    {
        instance = this;
        weaponSpawns = new List<WeaponSpawn>();
        animation = GetComponent<Animation>();
    }

    private void Start()
    {
        InstanciateWeapons();
    }

    public void InstanciateWeapons()
    {
        foreach (WeaponSpawn weaponSpawn in weaponSpawns)
        {
            weaponSpawn.InstanciateWeapon();
        }
    }

    public void ResetWeapons()
    {
        animation.Play("PushButton");

        foreach (WeaponSpawn weaponSpawn in weaponSpawns)
        {
            weaponSpawn.DestroyWeapon();
        }

        foreach (WeaponSpawn weaponSpawn in weaponSpawns)
        {
            weaponSpawn.InstanciateWeapon();
        }

        XRUIHandsBehavior.Instance.ItemIsNotHeld("Left");
        XRUIHandsBehavior.Instance.ItemIsNotHeld("Right");
    }

    public void AddSpawnerToList(WeaponSpawn weaponSpawn)
    {
        weaponSpawns.Add(weaponSpawn);
    }    
}
