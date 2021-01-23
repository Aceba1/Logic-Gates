using UnityEngine;

class NodeNot : LogicNode
{
    public override byte Inputs => 1;
    public override byte Outputs => 1;

    private float input;

    protected override void Calculate(LogicManager manager) =>
        SendSignal(0, (1f - Mathf.Abs(input)) * Mathf.Sign(input));

    protected override bool SetInput(int inputIndex, float value)
    {
        input = value;
        return true;
    }
}