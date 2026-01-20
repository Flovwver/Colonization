using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coal : VisitableTarget
{
    private Rigidbody _rigidbody;

    public event Action<Coal> OnRemoved;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Remove()
    {
        transform.SetParent(null, true);

        OnRemoved?.Invoke(this);
    }

    public void TurnOffKinematic()
    {
        _rigidbody.isKinematic = false;
    }

    public void TurnOnKinematic()
    {
        _rigidbody.isKinematic = true;
    }

    public override void Accept(TargetVisitor visitor) => visitor.Visit(this);
}
