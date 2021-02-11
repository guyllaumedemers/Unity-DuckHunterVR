using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IWeapon
{
    public float GunRange { get; set; }
    public float BulletTrailSize { get; set; }
    public float RateOfFire { get; set; }
    public float TimeBeforeNextShot { get; set; }
    public GameObject GunTip { get; set; }
    public LineRenderer BulletTrailPrefab { get; set; }
    public ParticleSystem MuzzleFlashParticles { get; set; }
    public ParticleSystem CartridgeEjectionParticles { get; set; }
    public AudioSource AudioSource { get; set; }
    public XRSocketInteractor XRSocketInteractor { get; set; }
    public SphereCollider AmmoReloadCollider { get; set; }
    public int CurrentAmmo { get; set; }
    public int MaxAmmo { get; set; }
    public bool HasClip { get; set; }
    public LayerMask GunHitLayers { get; set; }

    void Shoot();
    void DropClip();
    void InsertAmmo();
}
