using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : MonoBehaviour, IWeapon
{
    [field: SerializeField] public float GunRange { get; set; } = 50f;
    [field: SerializeField] public float BulletTrailSize { get; set; } = 0.01f;
    public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletTrailPrefab { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Start()
    {
        GunTip = GameObject.Find("SubmachineGunGunTip");
    }

    void Update()
    {

    }

    public void Shoot()
    {
        if (XRInputDebugger.Instance.inputDebugEnabled)
        {
            string debugMessage = name + " Shoot";
            Debug.Log(debugMessage);
            XRInputDebugger.Instance.DebugLogInGame(debugMessage);
        }
    }

    public void Reload()
    {
        if (XRInputDebugger.Instance.inputDebugEnabled)
        {
            string debugMessage = name + " Reload";
            Debug.Log(debugMessage);
            XRInputDebugger.Instance.DebugLogInGame(debugMessage);
        }
    }
}
