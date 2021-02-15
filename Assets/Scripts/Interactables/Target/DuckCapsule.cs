using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCapsule : MonoBehaviour, IShootable
{
    public void OnHit() {
        Debug.Log($"{this.name}");
        Destroy(gameObject);
    }
}
