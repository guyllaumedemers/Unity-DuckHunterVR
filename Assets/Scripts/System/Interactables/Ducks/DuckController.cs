using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckController : MonoBehaviour, IFlyingTarget, IShootable {
    
    [Header("PG-13 toggle")] 
    public bool isPg13 = true;
    [Header("Gore Particle")]
    public ParticleSystem particleBurst;
    [Header("Score points")]
    public float noPoints = 1f;
    [Header("Health Points")]
    public float HP = 1f;
    [Header("Flight Speed Velocity")]
    public float flightSpeed = 1f;
    [Header("Random Up Height increase")]
    public MinMax heighRangeIncrease = new MinMax(1f, 1.5f);
    [Header("GameObject Height limits")]
    public MinMax minMaxY = new MinMax(-1f, 5f);
    
    public Vector3 SpawnSize { get; set; }
    
    [SerializeField][Header("State of Duck")]
    private IFlyingTarget.State _state;
    private Vector3 _target;
    private Animation _animations;
    private SphereCollider _collider;
    private Rigidbody _rb;
    private bool _isDead;
    
    public void Start() {
        _animations = GetComponent<Animation>();
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        
        _animations.Play("fly");
        _target = GetRandomPosUp();
        
        minMaxY.max += SpawnSize.y;
        
        _isDead = false;
        _state = IFlyingTarget.State.FLYING;
    }
        
    public void OnHit() {
        HP--;
        StopCoroutine(nameof(PlayHitAnimations));
        StartCoroutine(nameof(PlayHitAnimations));
    }
    
    private void Update() {
        
        if (HP <= 0 && !_isDead) 
            _state = IFlyingTarget.State.DEAD;

        if(!_isDead){

            if(transform.position.y >= SpawnSize.y && _state != IFlyingTarget.State.FLEEING) {
                _collider.enabled = false;
                _target += Vector3.up * minMaxY.max;
                _state = IFlyingTarget.State.FLEEING;
            }
            
            switch (_state) {
                case IFlyingTarget.State.FLYING:
                    FlyAround();
                    break;
                
                case IFlyingTarget.State.FLEEING:
                    FlyToTarget();
                    break;
                
                case IFlyingTarget.State.DEAD:
                    StartCoroutine(nameof(DieRoutine));
                    break;
            }
        }
    }
    
    private void FixedUpdate() {
        if (transform.position.y <= minMaxY.min || transform.position.y >= minMaxY.max) {
            Destroy(gameObject);
        }
    }
    
    private void FlyAround() {
        FlyToTarget();
        if (Vector3.Distance(transform.position, _target) < 0.1f)
            _target = GetRandomPosUp();
    }

    private void FlyToTarget() {
        float step = flightSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);
        transform.LookAt(_target);
    }
    
    private Vector3 GetRandomPosUp() {
        float dirX = Random.Range(-SpawnSize.x / 2, SpawnSize.x / 2);
        float dirY = transform.position.y + Random.Range(heighRangeIncrease.min, heighRangeIncrease.max);
        float dirZ = Random.Range(-SpawnSize.z / 2, SpawnSize.z / 2);
        
        return new Vector3(dirX, dirY, dirZ);
    }
    
    private IEnumerator PlayHitAnimations() {
        _animations.Play("inAirDeath");
        yield return new WaitForSeconds(_animations["inAirDeath"].length);
        
        _animations.Play("fly");
        yield return null;
    }
    
    private IEnumerator DieRoutine() {
        _isDead = true;
        _collider.enabled = false;
        transform.position = transform.position;
        
        if (isPg13) {
            _animations.Play("inAirDeath");
            yield return new WaitForSeconds(_animations["inAirDeath"].length);
        
            _animations.Play("falling");
        }
        else {
            foreach (Transform child in transform) {
                if(!child.name.Contains("Explosion"))
                    child.gameObject.SetActive(false);
            }
            
            particleBurst.Play();
        }
        
        _rb.useGravity = true;
    }
}
