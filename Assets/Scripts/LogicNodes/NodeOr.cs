using System;
using UnityEngine;

public class NodeOr : BasicNode
{
    public override string Name => "OR Gate";

    protected override void Calculate() =>
        SendSignal(0, Mathf.Max(inputs));
}