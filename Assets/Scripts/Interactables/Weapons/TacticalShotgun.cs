using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalShotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private int nbPellets = 12;
    [SerializeField] private float pelletSpread = 0.1f;
    [field: SerializeField] public float GunRange { get; set; } = 50f;
    [field: SerializeField] public float BulletTrailSize { get; set; } = 0.005f;
    public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletTrailPrefab { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Start()
    {
        GunTip = GameObject.Find("TacticalShotgunGunTip");
    }

    public void Shoot()
    {
        for (int i = 0; i < nbPellets; i++)
        {
            Vector3 bullerDirection = GunTip.transform.TransformDirection(Vector3.forward);
            Vector3 spread = Random.insideUnitCircle * pelletSpread;
            bullerDirection += spread.x * GunTip.transform.right;
            bullerDirection += spread.y * GunTip.transform.up;

            Ray ray = new Ray(GunTip.transform.position, bullerDirection);
            RaycastHit raycastHit;
            Vector3 lineRendererEnd;

            if (Physics.Raycast(ray, out raycastHit, GunRange, GunHitLayers.value))
            {
                lineRendererEnd = raycastHit.point;

                if (XRInputDebugger.Instance.inputDebugEnabled)
                {
                    string debugMessage = "Raycast on: " + raycastHit.transform.name;
                    Debug.Log(debugMessage);
                    XRInputDebugger.Instance.DebugLogInGame(debugMessage);
                }
            }
            else
            {
                lineRendererEnd = ray.origin + ray.direction * GunRange;
            }

            LineRenderer bulletTrailClone = Instantiate(BulletTrailPrefab);
            bulletTrailClone.widthMultiplier = BulletTrailSize;
            bulletTrailClone.SetPositions(new Vector3[] { GunTip.transform.position, lineRendererEnd });

            StartCoroutine(LineRendererFade.Instance.FadeLineRenderer(bulletTrailClone));
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
