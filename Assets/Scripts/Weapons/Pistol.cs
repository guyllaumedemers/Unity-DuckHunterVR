using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol : MonoBehaviour, IWeaponComponents
{
    [field: SerializeField] public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletLineRenderer { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Reload()
    {
        XRInputDebugger.Instance.DebugLogInGame("Reload");
    }

    public void Shoot()
    {
        XRInputDebugger.Instance.DebugLogInGame("Shoot");
    }
}
