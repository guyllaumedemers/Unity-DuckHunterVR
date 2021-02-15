using UnityEngine;

public class TransformHolder
{
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _localScale;

    public TransformHolder(Transform t) {
        _position = t.position;
        _rotation = t.rotation;
        _localScale = t.localScale;
    }

    public void Store(Transform t) {
        _position = t.position;
        _rotation = t.rotation;
        _localScale = t.localScale;
    }

    public void Set(Transform t) {
        t.position = _position;
        t.rotation = _rotation;
        t.localScale = _localScale;
    }
}