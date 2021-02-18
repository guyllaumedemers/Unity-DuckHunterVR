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
    public float DestroyDistance { get; set; } = 0.2f;

    private void Start()
    {
        MagazineCanLoad = true;
        Rigidbody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        GrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Update()
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

            Destroy(gameObject, 3f);
        }

        CheckDistance();
    }

    public void CheckDistance()
    {
        if(gameObject.transform.parent != null)
        {
            float distance = Vector3.Distance(transform.position, gameObject.transform.parent.position);

            if (distance >= DestroyDistance)
            {
                gameObject.transform.parent = null;
            }
        }        
    }
}
