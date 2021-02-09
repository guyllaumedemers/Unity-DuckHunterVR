using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public GameObject GunTip { get; set; }
    public LineRenderer BulletLineRenderer { get; set; }
    public ParticleSystem MuzzleFlashParticles { get; set; }
    public ParticleSystem CartridgeEjectionParticles { get; set; }
    public LayerMask GunHitLayers { get; set; }

    void Shoot();
    void Reload();
}
