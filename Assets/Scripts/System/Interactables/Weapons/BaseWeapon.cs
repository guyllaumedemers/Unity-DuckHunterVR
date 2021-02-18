using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [field: SerializeField] public bool IsAcceptingMagazine { get; set; } = false;
    [field: SerializeField] public int NbBulletFired { get; set; } = 1;
    [field: SerializeField] public float BulletSpread { get; set; } = 0f;
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
    public AudioClip ShootingSound { get; set; }
    public AudioClip ReloadSound { get; set; }
    [field: SerializeField] public bool ReactorTriggerShoot { get; set; }
    protected abstract string ShootingSoundPath { get; }
    protected abstract string ReloadingSoundPath { get; }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        GunTip = GameObject.Find(name + "GunTip");

        if (IsAcceptingMagazine)
            MagazineAttach = AmmoReloadCollider.transform.GetChild(0);

        ShootingSound = Resources.Load<AudioClip>(ShootingSoundPath);
        ReloadSound = Resources.Load<AudioClip>(ReloadingSoundPath);
    }

    public virtual void Shoot()
    {
        if (AudioSource.clip != ShootingSound)
            AudioSource.clip = ShootingSound;

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

                        if (AudioSource.clip != ReloadSound)
                            AudioSource.clip = ReloadSound;

                        AudioSource.Play();

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
    }

    public virtual void DropMagazine()
    {
        if (CurrentMagazine != null)
        {
            //MeshRenderer mesh = CurrentMagazine.GetComponent<MeshRenderer>();

            

            CurrentAmmoContainer.MagazineCanLoad = false;
            CurrentAmmoContainer.TimeBeforeCanLoad = Time.time + CurrentAmmoContainer.TimeCanLoad;

            //CurrentAmmoContainer.CurrentAmmo = CurrentAmmo;
            //CurrentAmmo = 0;

            MagazineAttach.DetachChildren();

            CurrentAmmoContainer.BoxCollider.isTrigger = false;
            CurrentAmmoContainer.Rigidbody.isKinematic = false;
            CurrentAmmoContainer.Rigidbody.useGravity = true;

            CurrentAmmoContainer.GrabInteractable = CurrentAmmoContainer.gameObject.AddComponent<XRGrabInteractable>();

            CurrentAmmoContainer.GrabInteractable.movementType = XRGrabInteractable.MovementType.VelocityTracking;

            SelectionOutline selectionOutline = CurrentAmmoContainer.gameObject.GetComponent<SelectionOutline>();

            CurrentAmmoContainer.GrabInteractable.onFirstHoverEntered.AddListener(delegate { selectionOutline.Highlight(); });
            CurrentAmmoContainer.GrabInteractable.onLastHoverExited.AddListener(delegate { selectionOutline.RemoveHighlight(); });
            CurrentAmmoContainer.GrabInteractable.onSelectEntered.AddListener(delegate { selectionOutline.IsSelected(); });
            CurrentAmmoContainer.GrabInteractable.onSelectExited.AddListener(delegate { selectionOutline.IsNotSelected(); });


            CurrentAmmoContainer.GrabInteractable.attachTransform = CurrentMagazine.gameObject.transform.GetChild(0);

            CurrentMagazine = null;
            CurrentAmmoContainer = null;
        }
    }
}
