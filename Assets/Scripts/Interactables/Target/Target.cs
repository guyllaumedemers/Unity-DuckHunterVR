using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IShootable {
    
    
    public ParticleSystem[] particleBursts;
    
    private readonly IDictionary<GameObject, TransformHolder> _children = new Dictionary<GameObject, TransformHolder>();

    void Awake() {
        
        foreach (Transform child in transform) {
            if (!child.name.Contains("TargetExplosion")) {
                
                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
                    child.gameObject.AddComponent<Rigidbody>();
                    child.gameObject.AddComponent<BoxCollider>();
                    
                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().isKinematic = true;

                    child.GetComponent<BoxCollider>().size = new Vector3(1.2f, 1.2f, 1.2f);
                    child.GetComponent<BoxCollider>().enabled = false;
                }
                
                _children.Add(child.gameObject, new TransformHolder(child.GetComponent<Transform>()));
            }
        }
    }

    public void Start() {
        //Invoke(nameof(OnHit), 4f);
    }

    public void OnHit() {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            StartCoroutine(nameof(OnHitPhysics));
        else if (Application.platform == RuntimePlatform.Android)
            StartCoroutine(nameof(OnHitParticles));
    }

    private IEnumerator OnHitPhysics() {

        ExplodePhysics();
        yield return new WaitForSeconds(4);
        DisablePhysics();
        yield return new WaitForSeconds(2);
        EnablePhysics();
        yield return null;
    }

    private IEnumerator OnHitParticles() {
        ExplodeParticles();
        yield return new WaitForSeconds(1);
        DisableParticles();
        yield return new WaitForSeconds(2);
        EnableParticles();
        yield return null;
    }

    private void ExplodePhysics() {
        foreach (var d in _children) {
            d.Key.GetComponent<BoxCollider>().enabled = true;
            d.Key.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void ExplodeParticles() {
        foreach (var d in _children)
            d.Key.SetActive(false);

        foreach (var p in particleBursts)
            p.Play();
    }
    
    private void DisablePhysics() {
        foreach (var d in _children) {
            d.Key.GetComponent<Rigidbody>().isKinematic = true;
            d.Key.GetComponent<BoxCollider>().enabled = false;
            d.Key.SetActive(false);
        }
    }

    private void DisableParticles() {
        foreach (var d in _children) {
            d.Key.SetActive(false);
        }
    }

    private void EnablePhysics() {
        foreach (var d in _children) {
            d.Key.SetActive(true);
            d.Value.Set(d.Key.GetComponent<Transform>());
        }
    }
    
    private void EnableParticles() {
        foreach (var d in _children)
            d.Key.SetActive(true);
    }
}
