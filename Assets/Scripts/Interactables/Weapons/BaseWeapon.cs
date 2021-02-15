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
    public Transform MagazineAttach { get; set; }
    [field: SerializeField] public GameObject CurrentMagazine { get; set; }
    [field: SerializeField] public AmmoContainer CurrentAmmoContainer { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        GunTip = GameObject.Find(name + "GunTip");

        if (IsAcceptingMagazine)
            MagazineAttach = AmmoReloadCollider.transform.GetChild(0);
    }

    public virtual void Shoot()
    {
        if (Time.time >= TimeBeforeNextShot)
        {
            if (IsAcceptingMagazine && CurrentAmmoContainer != null)
            {
                if (CurrentAmmoContainer.CurrentAmmo > 0)
                {
                    CreateBullets();
                    CurrentAmmoContainer.CurrentAmmo--;
                }
            }
            else
            {
                if (CurrentAmmo > 0)
                {
                    CreateBullets();
                    CurrentAmmo--;
                }
            }

            TimeBeforeNextShot = Time.time + RateOfFire;
        }
    }

    public void CreateBullets()
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
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (IsAcceptingMagazine)
        {
            if (CurrentMagazine == null)
            {
                if (collider.gameObject.CompareTag(name + "Magazine") && AmmoReloadCollider.CompareTag(name + "Reload"))
                {
                    CurrentMagazine = collider.gameObject;

                    CurrentAmmoContainer = collider.GetComponent<AmmoContainer>();

                    if (CurrentAmmoContainer.MagazineCanLoad)
                    {
                        CurrentAmmoContainer.Rigidbody.isKinematic = true;
                        CurrentAmmoContainer.BoxCollider.isTrigger = true;

                        CurrentMagazine.transform.SetParent(MagazineAttach.transform);

                        CurrentMagazine.transform.position = new Vector3(MagazineAttach.transform.position.x, MagazineAttach.transform.position.y, MagazineAttach.transform.position.z);

                        CurrentMagazine.transform.rotation = MagazineAttach.transform.rotation;

                        GameObject magazineClone = Instantiate(CurrentMagazine, CurrentMagazine.transform.position, CurrentMagazine.transform.rotation, MagazineAttach.transform);
                        magazineClone.transform.name = collider.name.Replace("(clone)", "").Trim();

                        if (CurrentMagazine.name != null)
                        {
                            Destroy(CurrentMagazine);
                            CurrentMagazine = magazineClone;
                            CurrentAmmoContainer = CurrentMagazine.GetComponent<AmmoContainer>();
                            Destroy(magazineClone.GetComponent<XRGrabInteractable>());
                        }

                        //CurrentAmmo = CurrentAmmoContainer.CurrentAmmo;
                    }
                    else
                    {
                        CurrentMagazine = null;
                        CurrentAmmoContainer = null;
                    }
                }
            }
        }
        else if (collider.gameObject.CompareTag(name + "AmmoBox") && AmmoReloadCollider.CompareTag(name + "Reload"))
        {
            CurrentAmmoContainer = collider.GetComponent<AmmoContainer>();

            int ammoNeeded = MaxAmmo - CurrentAmmo;
            //int ammoRemaining = CurrentAmmoContainer.CurrentAmmo;

            for (int i = 0; i < ammoNeeded; i++)
            {
                if (CurrentAmmoContainer.CurrentAmmo != 0)
                {
                    CurrentAmmo++;
                    CurrentAmmoContainer.CurrentAmmo--;
                }
            }


            CurrentAmmoContainer = null;
            //CurrentAmmo = MaxAmmo;
        }
    }

    public virtual void DropMagazine()
    {
        if (CurrentMagazine != null)
        {
            CurrentAmmoContainer.MagazineCanLoad = false;
            CurrentAmmoContainer.TimeBeforeCanLoad = Time.time + CurrentAmmoContainer.TimeCanLoad;

            //CurrentAmmoContainer.CurrentAmmo = CurrentAmmo;
            //CurrentAmmo = 0;

            MagazineAttach.DetachChildren();

            CurrentAmmoContainer.BoxCollider.isTrigger = false;
            CurrentAmmoContainer.Rigidbody.isKinematic = false;
            CurrentAmmoContainer.Rigidbody.useGravity = true;

            CurrentAmmoContainer.GrabInteractable = CurrentAmmoContainer.gameObject.AddComponent<XRGrabInteractable>();
            CurrentAmmoContainer.GrabInteractable.attachTransform = CurrentMagazine.gameObject.transform.GetChild(0);

            CurrentMagazine = null;
            CurrentAmmoContainer = null;
        }
    }
}
