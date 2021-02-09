using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalShotgun : MonoBehaviour, IWeapon
{
    [field: SerializeField] public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletLineRenderer { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Start()
    {
        
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
