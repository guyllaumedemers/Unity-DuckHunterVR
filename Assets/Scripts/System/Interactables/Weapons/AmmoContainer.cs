using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AmmoContainer : MonoBehaviour, IAmmoContainer
{
    public Rigidbody Rigidbody { get; set; }
    public BoxCollider BoxCollider { get; set; }
    [field: SerializeField] public int CurrentAmmo { get; set; }
    public bool MagazineCanLoad { get; set; }
    [field: SerializeField] public bool IsMagazine { get; set; }
    public float TimeCanLoad { get; set; } = 0.5f;
    public float TimeBeforeCanLoad { get; set; } = 0f;
    public XRGrabInteractable GrabInteractable { get; set; }
    public Transform ammoContainerParent { get; set; }
    public bool _isBeingDestroyed { get; set; }
    [field: SerializeField] public float TimeToDestroy { get; set; } = 120f;
    public float TimeBeforeDestroy { get; set; }

    void Start()
    {
        MagazineCanLoad = true;
        Rigidbody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        GrabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (gameObject.transform.parent == null && !_isBeingDestroyed)
        {
            Debug.Log("No parent");
            TimeBeforeDestroy = Time.time + TimeToDestroy;
            _isBeingDestroyed = true;
        }

        if (_isBeingDestroyed)
        {
            if (Time.time >= TimeBeforeDestroy)
                Destroy(gameObject);
        }

        if (!MagazineCanLoad && CurrentAmmo > 0)
        {
            if (Time.time >= TimeBeforeCanLoad)
            {
                MagazineCanLoad = true;
            }
        }
        else if (!MagazineCanLoad && CurrentAmmo == 0 || !IsMagazine && CurrentAmmo == 0)
        {
            GrabInteractable.onFirstHoverEntered.RemoveAllListeners();
            GrabInteractable.onLastHoverExited.RemoveAllListeners();
            GrabInteractable.onSelectEntered.RemoveAllListeners();
            GrabInteractable.onSelectExited.RemoveAllListeners();

            Destroy(gameObject, 3f);
        }
    }
}
