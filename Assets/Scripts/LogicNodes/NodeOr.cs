using System;
using UnityEngine;

public class NodeOr : BasicNode
{
    public override string Name => "OR Gate";

    protected override void Calculate(LogicManager manager) =>
        SendSignal(0, Mathf.Max(inputs));
}