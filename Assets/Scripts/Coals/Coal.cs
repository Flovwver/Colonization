using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coal : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event Action<Coal> OnRemoved;

    public bool IsTook { get; private set; } = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Remove()
    {
        transform.SetParent(null, true);
        IsTook = false;

        OnRemoved?.Invoke(this);
    }

    public void OnInitialize()
    {
        _rigidbody.isKinematic = false;
    }

    public void OnTook()
    {
        IsTook = true;
        _rigidbody.isKinematic = true;
    }
}
