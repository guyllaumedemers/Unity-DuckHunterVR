using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [field: SerializeField] public int CurrentAmmo { get; set; } = 0;
    [field: SerializeField] public int MaxAmmo { get; set; } = 10;
    [field: SerializeField] public bool IsAcceptingMagazine { get; set; } = false;
    [field: SerializeField] public int NbBulletFired { get; set; } = 1;
    [field: SerializeField] public float BulletSpread { get; set; } = 0f;
    [field: SerializeField] public float RateOfFire { get; set; } = 0f;
    public float TimeBeforeNextShot { get; set; } = 0f;
    [field: SerializeField] public float GunRange { get; set; } = 50f;
    [field: SerializeField] public float BulletTrailSize { get; set; } = 0.1f;
    public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletTrailPrefab { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    public AudioSource AudioSource { get; set; }
    [field: SerializeField] public SphereCollider AmmoReloadCollider { get; set; }
    [field: SerializeField] public GameObject CurrentMagazine { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        GunTip = GameObject.Find(name + "GunTip");
        AmmoReloadCollider = GameObject.FindGameObjectWithTag(name + "Reload").GetComponent<SphereCollider>();
    }

    public virtual void Shoot()
    {
        if (Time.time >= TimeBeforeNextShot)
        {
            if (CurrentAmmo > 0)
            {
                AudioSource.Play();
                MuzzleFlashParticles.Play();
                CartridgeEjectionParticles.Play();

                for (int i = 0; i < NbBulletFired; i++)
                {
                    Vector3 bulletDirection = GunTip.transform.TransformDirection(Vector3.forward);
                    Vector3 spread = Random.insideUnitCircle * BulletSpread;
                    bulletDirection += spread.x * GunTip.transform.right;
                    bulletDirection += spread.y * GunTip.transform.up;

                    Ray ray = new Ray(GunTip.transform.position, bulletDirection);
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

                        if (raycastHit.collider.GetComponent<IShootable>() != null)
                        {
                            raycastHit.collider.GetComponent<IShootable>().OnHit();
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

                CurrentAmmo--;

                TimeBeforeNextShot = Time.time + RateOfFire;
            }
        }
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (IsAcceptingMagazine)
        {
            if (CurrentMagazine == null)
            {
                if (collider.gameObject.tag.Contains(name + "Magazine") && AmmoReloadCollider.tag == name + "Reload")
                {
                    CurrentMagazine = collider.gameObject;

                    Rigidbody currentMagazineRb = CurrentMagazine.GetComponent<Rigidbody>();
                    BoxCollider currentMagazineBc = CurrentMagazine.GetComponent<BoxCollider>();

                    currentMagazineRb.isKinematic = true;
                    currentMagazineBc.isTrigger = true;

                    Transform magazineAttach = GameObject.FindGameObjectWithTag(name + "Reload").transform.GetChild(0);

                    CurrentMagazine.transform.SetParent(magazineAttach.transform);

                    CurrentMagazine.transform.position = new Vector3(magazineAttach.transform.position.x, magazineAttach.transform.position.y, magazineAttach.transform.position.z);

                    CurrentMagazine.transform.rotation = magazineAttach.transform.rotation;

                    GameObject magazineClone = Instantiate(CurrentMagazine, CurrentMagazine.transform.position, CurrentMagazine.transform.rotation, magazineAttach.transform);

                    magazineClone.transform.name = collider.name.Replace("(clone)", "").Trim();

                    XRGrabInteractable magazineGrab = magazineClone.GetComponent<XRGrabInteractable>();

                    if (CurrentMagazine.name != null)
                    {
                        Destroy(CurrentMagazine);
                        CurrentMagazine = magazineClone;
                        Destroy(magazineGrab);
                    }
                }
            }
        }
        else
        {
            if (collider.gameObject.tag == name + "AmmoBox" && AmmoReloadCollider.tag == name + "Reload")
            {
                CurrentAmmo = MaxAmmo;
            }
        }
    }

    public virtual void DropMagazine()
    {
        if (CurrentMagazine != null)
        {
            CurrentMagazine.tag = "Untagged";

            Transform magazineAttach = GameObject.FindGameObjectWithTag(name + "Reload").transform.GetChild(0);
            magazineAttach.DetachChildren();

            BoxCollider magazineBc = CurrentMagazine.GetComponent<BoxCollider>();
            magazineBc.isTrigger = false;

            Rigidbody magazineRb = CurrentMagazine.GetComponent<Rigidbody>();
            magazineRb.isKinematic = false;
            magazineRb.useGravity = true;

            XRGrabInteractable magazineGrab = CurrentMagazine.AddComponent<XRGrabInteractable>();
            magazineGrab.attachTransform = CurrentMagazine.gameObject.transform.GetChild(0);

            CurrentMagazine = null;
        }
    }
}
