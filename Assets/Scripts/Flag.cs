using UnityEngine;

public class Flag : VisitableTarget
{
    public override void Accept(TargetVisitor visitor) => visitor.Visit(this);
}
