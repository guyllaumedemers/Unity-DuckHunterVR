using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DuckController : MonoBehaviour, IShootable {
    
    public float noPoints = 1f;
    public float flightSpeed = 2f;
    
    private Animation _animations;
    private SphereCollider _collider;
    private Rigidbody _rb;

    public void Start() {
        _animations = GetComponent<Animation>();
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();

        _collider.enabled = false;
        _rb.detectCollisions = false;
        
        // Play Quack Quack sound
        
        Invoke(nameof(Fly), 2f);
    }

    public void OnHit() {
        Debug.Log($"{name} killed");
        //Add noPoints to score
        StartCoroutine(nameof(PlayDeathAnimations));
    }

    private void Fly() {
        _collider.enabled = true;
    }

    private void FlyAway() {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3)
            Destroy(gameObject);
    }
    
    private IEnumerator PlayDeathAnimations() {
        _animations.Play("inAirDeath");
        yield return new WaitForSeconds(_animations["inAirDeath"].length);
        
        _animations.Play("falling");
        _rb.detectCollisions = true;
        _rb.useGravity = true;
        yield return null;
    }
}
