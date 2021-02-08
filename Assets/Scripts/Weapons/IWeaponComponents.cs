using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponComponents
{
    public GameObject GunTip { get; set; }
    public LineRenderer BulletLineRenderer { get; set; }
    public ParticleSystem MuzzleFlashParticles { get; set; }
    public ParticleSystem CartridgeEjectionParticles { get; set; }

    void Shoot();
    void Reload();
}
