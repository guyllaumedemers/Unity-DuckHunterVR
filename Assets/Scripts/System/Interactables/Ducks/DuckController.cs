using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable] 
public class MinMax {
    public float min;
    public float max;

    public MinMax(float min, float max) {
        this.min = min;
        this.max = max;
    }
}

public class DuckController : MonoBehaviour, IShootable {

    private enum State {
        FLYING,
        FLEEING,
        DEAD
    }

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
    [Header("Time to escape")]
    public float escapeTime = 10f;
    [Header("Random Up Height increase")]
    public MinMax heighRangeIncrease = new MinMax(1f, 1.5f);
    [Header("GameObject Height limits")]
    public MinMax minMaxY = new MinMax(-1f, 10f);
    
    [HideInInspector]
    public float spawnSize;
    
    [SerializeField][Header("State of Duck")]
    private State _state;
    [SerializeField][Header("Current Duck Target")]
    private Vector3 _target;
    private Animation _animations;
    private SphereCollider _collider;
    private Rigidbody _rb;
    private bool _isDead = false;
    
    public void Start() {
        _animations = GetComponent<Animation>();
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        
        _animations.Play("fly");
        _target = GetRandomPosUp();
        _state = State.FLYING;
    }
        
    public void OnHit() {
        HP--;
        StopCoroutine(nameof(PlayHitAnimations));
        StartCoroutine(nameof(PlayHitAnimations));
    }
    
    private void Update() {
        
        if (HP <= 0 && !_isDead) 
            _state = State.DEAD;

        if(!_isDead){

            if (escapeTime <= 0 && _state != State.FLEEING) {
                _collider.enabled = false;
                _target = new Vector3(transform.position.x, minMaxY.max, transform.position.z);
                _state = State.FLEEING;
            }
            
            switch (_state) {
                case State.FLYING:
                    FlyAround();
                    escapeTime -= Time.deltaTime;
                    break;
                
                case State.FLEEING:
                    FlyToTarget();
                    break;
                
                case State.DEAD:
                    StartCoroutine(nameof(Die));
                    break;
            }
        }
    }
    
    private void FixedUpdate() {
        if (transform.position.y <= minMaxY.min || transform.position.y >= minMaxY.max)
            Destroy(gameObject);
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
        float x = Random.Range(0, spawnSize);
        float dirX = transform.position.x >= 0 ? -x : x;
        float dirY = transform.position.y + Random.Range(heighRangeIncrease.min, heighRangeIncrease.max);
        
        return new Vector3(dirX, dirY, transform.position.z);
    }

    private IEnumerator PlayHitAnimations() {
        _animations.Play("inAirDeath");
        yield return new WaitForSeconds(_animations["inAirDeath"].length);
        
        _animations.Play("fly");
        yield return null;
    }
    
    private IEnumerator Die() {
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
        yield return null;
    }
}
