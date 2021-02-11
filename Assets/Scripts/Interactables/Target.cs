using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private readonly IDictionary<GameObject, TransformHolder> _children = new Dictionary<GameObject, TransformHolder>();
    
    void Awake() {
        foreach (Transform child in transform) {
            _children.Add(child.gameObject, new TransformHolder(child.GetComponent<Transform>()));
        }
    }

    public void Start() {
        //Invoke(nameof(OnHit), 2f);
    }
    
    public void OnHit() {
        StartCoroutine(nameof(ExplodeAndDisable));
    }
    
    IEnumerator ExplodeAndDisable() {
        foreach (var d in _children) {
            d.Key.GetComponent<BoxCollider>().enabled = true;
            d.Key.GetComponent<Rigidbody>().isKinematic = false;
        }
        
        yield return new WaitForSeconds(2);
        
        foreach (var d in _children) {
            d.Key.GetComponent<Rigidbody>().isKinematic = true;
            d.Key.GetComponent<BoxCollider>().enabled = false;
            d.Key.SetActive(false);
        }
        
        yield return new WaitForSeconds(2);
        
        foreach (var d in _children) {
            d.Key.SetActive(true);
            d.Value.Set(d.Key.GetComponent<Transform>());
        }
        
        yield return null;
    }
}
