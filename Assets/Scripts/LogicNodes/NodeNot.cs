using System;
using UnityEngine;

public class NodeNot : LogicNode
{
    public override byte Inputs => 1;
    public override byte Outputs => 1;

    private float input;

    protected override void Calculate() =>
        SendSignal(0, (1f - Mathf.Abs(input)) * Mathf.Sign(input));

    public override void Disconnect(int inputIndex) => 
        input = 0f;

    protected override Response SetInput(int inputIndex, float value)
    {
        if (input == value) 
            return Response.None;
        input = value;
        return Response.Ready;
    }
}