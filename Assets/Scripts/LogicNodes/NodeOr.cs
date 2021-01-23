using UnityEngine;

class NodeOr : BasicNode
{
    public override string Name => "OR Gate";

    protected override void Calculate(LogicManager manager) =>
        SendSignal(0, Mathf.Min(inputs));
}