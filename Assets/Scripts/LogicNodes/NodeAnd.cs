using System;
using UnityEngine;

public class NodeAnd : BasicNode
{
    public override string Name => "AND Gate";

    protected override void Calculate(LogicManager manager) =>
        SendSignal(0, Mathf.Min(inputs));
}