using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class DesertEagle : BaseWeapon
{
    public GameObject currentClip;
    void Awake()
    {
        GunTip = GameObject.Find("DesertEagleGunTip");
        AmmoReloadCollider = GameObject.FindGameObjectWithTag("PistolReload").GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if(currentClip == null)
        {

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Contains("PistolClip") && AmmoReloadCollider.tag == "PistolReload")
        {
            currentClip = collider.gameObject;

            if(currentClip != null)
            {
                Rigidbody rb = currentClip.GetComponent<Rigidbody>();
                BoxCollider bc = currentClip.GetComponent<BoxCollider>();

                GameObject clipAttach = GameObject.Find("ClipAttach");

                collider.gameObject.transform.SetParent(clipAttach.transform);

                rb.isKinematic = true;

                bc.isTrigger = true;

                collider.gameObject.transform.position = new Vector3(clipAttach.transform.position.x, clipAttach.transform.position.y, clipAttach.transform.position.z);

                currentClip.transform.rotation = clipAttach.transform.rotation;

                XRGrabInteractable grab = currentClip.GetComponent<XRGrabInteractable>();

                Debug.Log(currentClip.name);

                if (currentClip.name != null)
                {
                    Destroy(grab);
                }
            }
        } 
    }

    /*private void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag.Contains("PistolClip") && AmmoReloadCollider.tag == "PistolReload")
        {
            Debug.Log("TriggerExit");

            currentClip = null;
        }        
    }*/

    public override void DropClip()
    {
        if(currentClip != null)
        {
            Transform reloadSpot = GameObject.Find("ClipAttach").transform;
            reloadSpot.DetachChildren();

            BoxCollider bc = currentClip.GetComponent<BoxCollider>();
            bc.enabled = false;

            Rigidbody clipRb = currentClip.GetComponent<Rigidbody>();
            clipRb.isKinematic = false;

            Destroy(currentClip.gameObject, 1f);
            currentClip = null;
            
        }
    }
}
