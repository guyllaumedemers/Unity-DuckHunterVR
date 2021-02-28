using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IShootable, IFlyingTarget
{
    public bool usePhysics;
    public ParticleSystem[] particleBursts;
    [Header("Target Movement")]
    [SerializeField] private bool _moveLeftAndRight = false;
    [SerializeField] private bool _moveUpAndDown = false;
    public float moveRange;
    private Vector3 _originalPosition;
    private bool _moveRight;
    private bool _moveDown;
    public float flightSpeed = 2f;

    private readonly IDictionary<GameObject, TransformHolder> _children = new Dictionary<GameObject, TransformHolder>();

    public IFlyingTarget.DieDelegate DiedDelegate { get; set; }
    public Vector3 SpawnerPos { get; set; }
    public Vector3 SpawnSize { get; set; }
    public float FlightSpeed { get => flightSpeed; set => flightSpeed = value; }

    void Awake()
    {
        _originalPosition = transform.position;

        do
        {
            float randomMoveRight = UnityEngine.Random.Range(0f, 1f);
            float randomMoveDown = UnityEngine.Random.Range(0f, 1f);

            _moveLeftAndRight = (randomMoveRight < 0.5f);
            _moveUpAndDown = (randomMoveDown < 0.5f);
        } while (_moveLeftAndRight == false && _moveUpAndDown == false);

        foreach (Transform child in transform)
        {
            if (!child.name.Contains("TargetExplosion"))
            {

                //if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                //{
                    child.gameObject.AddComponent<Rigidbody>();
                    child.gameObject.AddComponent<BoxCollider>();

                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().isKinematic = true;

                    child.GetComponent<BoxCollider>().size = new Vector3(1.2f, 1.2f, 1.2f);
                    child.GetComponent<BoxCollider>().enabled = false;
                //}

                _children.Add(child.gameObject, new TransformHolder(child.GetComponent<Transform>()));
            }
        }
    }

    public void OnHit()
    {
        DiedDelegate?.Invoke();

        _moveLeftAndRight = false;
        _moveUpAndDown = false;

        //if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor && usePhysics)
            StartCoroutine(nameof(OnHitPhysics));
        //else if (Application.platform == RuntimePlatform.Android || !usePhysics)
        //    StartCoroutine(nameof(OnHitParticles));
    }

    private IEnumerator OnHitPhysics()
    {
        ExplodePhysics();
        yield return new WaitForSeconds(1);
        DisablePhysics();
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    private IEnumerator OnHitParticles()
    {
        ExplodeParticles();
        yield return new WaitForSeconds(1);
        DisableParticles();
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    private void ExplodePhysics()
    {
        foreach (var d in _children)
        {
            d.Key.GetComponent<BoxCollider>().enabled = true;
            d.Key.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void ExplodeParticles()
    {
        foreach (var d in _children)
            d.Key.SetActive(false);

        foreach (var p in particleBursts)
            p.Play();
    }

    private void DisablePhysics()
    {
        foreach (var d in _children)
        {
            d.Key.GetComponent<Rigidbody>().isKinematic = true;
            d.Key.GetComponent<BoxCollider>().enabled = false;
            d.Key.SetActive(false);
        }
    }

    private void DisableParticles()
    {
        foreach (var d in _children)
            d.Key.SetActive(false);
    }

    /*private void EnablePhysics()
    {
        foreach (var d in _children)
        {
            d.Key.SetActive(true);
            d.Value.Set(d.Key.GetComponent<Transform>());
        }
    }

    private void EnableParticles()
    {
        foreach (var d in _children)
            d.Key.SetActive(true);
    }*/

    private void Update()
    {
        if (_moveLeftAndRight)
            MoveLeftAndRight();

        if (_moveUpAndDown)
            MoveUpAndDown();
    }

    private void MoveUpAndDown()
    {
        if (_moveDown == true)
        {
            if (transform.position.y <= _originalPosition.y + moveRange)
                transform.position += new Vector3(0, flightSpeed * Time.deltaTime, 0);
            else
                _moveDown = false;
        }
        else
        {
            if (transform.position.y >= _originalPosition.y)
                transform.position -= new Vector3(0, flightSpeed * Time.deltaTime, 0);
            else
                _moveDown = true;
        }
    }

    private void MoveLeftAndRight()
    {
        if (_moveRight == true)
        {
            if (transform.position.z <= _originalPosition.z + moveRange)
                transform.position += new Vector3(0, 0, flightSpeed * Time.deltaTime);
            else
                _moveRight = false;
        }
        else
        {
            if (transform.position.z >= _originalPosition.z)
                transform.position -= new Vector3(0, 0, flightSpeed * Time.deltaTime);
            else
                _moveRight = true;
        }
    }
}
