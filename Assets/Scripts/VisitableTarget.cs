using UnityEngine;

public abstract class VisitableTarget : MonoBehaviour
{
    public abstract void Accept(TargetVisitor visitor);
}
