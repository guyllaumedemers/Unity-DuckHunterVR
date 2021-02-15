using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DuckController : MonoBehaviour, IShootable {
    
    public float noPoints = 1f;
    public float flightSpeed = 2f;
    
    private Animation _animations;
    
    public void Start() {
        _animations = GetComponent<Animation>();
    }

    public void OnHit() {
        Debug.Log($"{name} killed");
        //Add noPoints to score
        StartCoroutine(nameof(PlayDeathAnimations));
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
    
    private IEnumerator PlayDeathAnimations() {
        _animations.Play("inAirDeath");
        yield return new WaitForSeconds(_animations["inAirDeath"].length);
        
        _animations.Play("falling");
        gameObject.AddComponent<Rigidbody>().useGravity = true;
        yield return null;
    }
}
