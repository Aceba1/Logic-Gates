using UnityEngine;

class NodeAnd : BasicNode
{
    public override string Name => "AND Gate";

    protected override void Calculate(LogicManager manager) =>
        SendSignal(0, Mathf.Max(inputs));
}