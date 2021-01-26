using System;
using UnityEngine;

public class NodeAnd : BasicNode
{
    public override string Name => "AND Gate";

    protected override void Calculate() =>
        SendSignal(0, Mathf.Min(inputs));
}