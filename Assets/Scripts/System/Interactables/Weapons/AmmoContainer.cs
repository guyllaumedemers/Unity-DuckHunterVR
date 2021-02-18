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
    [field: SerializeField] public float DestroyDistance { get; set; } = 25f;

    void Start()
    {
        MagazineCanLoad = true;
        Rigidbody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        GrabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
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

            Destroy(this.gameObject, 3f);
        }

        CheckDistance();
    }


    private void CheckDistance()
    {
        Transform parentTransform = ammoContainerParent;
        float distance = Vector3.Distance(transform.position, parentTransform.position);

        if (distance >= DestroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
