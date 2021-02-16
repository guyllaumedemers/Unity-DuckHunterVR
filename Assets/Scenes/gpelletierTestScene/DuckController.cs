using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckController : MonoBehaviour, IShootable {

    public float noPoints = 1f;
    public float flightSpeed = 2f;
    public float escapeTime = 2f;
    //public Vector2 flightRange = {1f, 1.5f};
    
    [HideInInspector]
    public Vector2 spawnZone;
    
    private Animation _animations;
    private SphereCollider _collider;
    private Rigidbody _rb;
    private Vector3 _target;
    private bool _isAlive = true;
    private bool _isFalling = false;
    
    public void Start() {
        _animations = GetComponent<Animation>();
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();

        _rb.detectCollisions = false;
        _animations.Play("fly");
        
        _target = GetRandomPosUp();
    }
        
    public void OnHit() {
        Debug.Log($"{name} killed");
        //Add noPoints to score
        _isAlive = false;
        StartCoroutine(nameof(Die));
    }
    
    private void Update() {
        if (_isAlive && escapeTime >= 0) {
            FlyAround();
            escapeTime -= Time.deltaTime;
        }
        else if (!_isFalling){
            StartCoroutine(nameof(FlyAway));
        }
    }
    
    private void FixedUpdate() {
        if (transform.position.y <= -1 || transform.position.y >= 10) {
            Destroy(gameObject);
        }
    }
    
    private void FlyAround() {
        float step = flightSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);
        
        if (Vector3.Distance(transform.position, _target) < 0.1f) {
            _target = GetRandomPosUp();
        }
    }

    private Vector3 GetRandomPosUp() {
        float direction = transform.position.x > 0 ? spawnZone.x : spawnZone.y;
        Vector3 pos = new Vector3(Random.Range(0, direction), transform.position.y + Random.Range(1f, 1.5f), transform.position.z);
        transform.LookAt(pos);
        return pos;
    }
    
    private IEnumerator FlyAway() {
        _collider.enabled = false;
        _target = new Vector3(transform.position.x,11, transform.position.z);
        transform.LookAt(_target);
        
        while (Vector3.Distance(transform.position, _target) > 0.1f) {
            float step = flightSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target, step);
            yield return new WaitForSeconds(2f);
        }
        
        yield return null;
    }
    
    private IEnumerator Die() {
        _isFalling = true;
        _collider.enabled = false;
        _animations.Play("inAirDeath");
        yield return new WaitForSeconds(_animations["inAirDeath"].length);
        
        _animations.Play("falling");
        _rb.useGravity = true;
        yield return null;
    }
}
