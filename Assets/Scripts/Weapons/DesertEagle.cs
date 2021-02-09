using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DesertEagle : MonoBehaviour, IWeapon
{
    public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletLineRenderer { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Awake()
    {
        GunTip = GameObject.Find("DesertEagleGunTip");
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Shoot()
    {
        float weaponRange = 50f;

        Ray ray = new Ray(GunTip.transform.position, GunTip.transform.TransformDirection(Vector3.forward));
        RaycastHit raycastHit;
        Vector3 lineRendererEnd;

        if (Physics.Raycast(ray, out raycastHit, weaponRange, GunHitLayers.value))
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
            lineRendererEnd = ray.origin + ray.direction * weaponRange;
        }        

        LineRenderer bulletTrailClone = Instantiate(BulletLineRenderer);
        bulletTrailClone.SetPositions(new Vector3[] { GunTip.transform.position, lineRendererEnd });

        StartCoroutine(LineRendererFade.Instance.FadeLineRenderer(bulletTrailClone));
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
