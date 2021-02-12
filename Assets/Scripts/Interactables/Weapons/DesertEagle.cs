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
        Transform reloadSpot = GameObject.Find("ClipAttach").transform;

        if(reloadSpot.childCount == 0)
        {
            currentClip = null;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (currentClip == null)
        {
            if (collider.gameObject.tag.Contains("PistolClip") && AmmoReloadCollider.tag == "PistolReload")
            {
                currentClip = collider.gameObject;

                Rigidbody rb = currentClip.GetComponent<Rigidbody>();
                BoxCollider bc = currentClip.GetComponent<BoxCollider>();               
                
                bc.isTrigger = true;
                rb.isKinematic = true;                

                GameObject clipAttach = GameObject.Find("ClipAttach");

                collider.gameObject.transform.SetParent(clipAttach.transform);

                Debug.Log(bc.gameObject.name);

                //Debug.Log(bc.isTrigger);

                collider.gameObject.transform.position = new Vector3(clipAttach.transform.position.x, clipAttach.transform.position.y, clipAttach.transform.position.z);

                currentClip.transform.rotation = clipAttach.transform.rotation;

                XRGrabInteractable grab = currentClip.GetComponent<XRGrabInteractable>();

                //Debug.Log(currentClip.name);

                if (currentClip.name != null)
                {
                    //Destroy(grab);
                }
            }
        }
    }

    /*private void OnTriggerExit(Collider collider)
    {
        if(collider == null && currentClip != null)
        {
            BoxCollider bc = currentClip.GetComponent<BoxCollider>();
            bc.enabled = true;
        }        
    }*/

    public override void DropClip()
    {
        if (currentClip != null)
        {
            Transform reloadSpot = GameObject.Find("ClipAttach").transform;
            reloadSpot.DetachChildren();

            BoxCollider bc = currentClip.GetComponent<BoxCollider>();
            bc.isTrigger = false;

            Rigidbody clipRb = currentClip.GetComponent<Rigidbody>();
            clipRb.isKinematic = false;

            //Destroy(currentClip.gameObject, 1f);
            

        }
    }
}
