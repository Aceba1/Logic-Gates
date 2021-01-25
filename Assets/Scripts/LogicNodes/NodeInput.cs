using System;
using UnityEngine;

public class NodeInput : LogicNode
{
    public override byte Inputs => 0;
    public override byte Outputs => 1;

    public KeyCode key = KeyCode.Space;

    public NodeInput(KeyCode key)
    {
        this.key = key;
    }

    int lastValue = 0;

    protected override void Calculate(LogicManager manager)
    {
        int thisValue = Input.GetKey(key) ? 1 : 0;
        if (thisValue != lastValue)
        {
            SendSignal(0, thisValue);
            lastValue = thisValue;
        }
        MarkActive();
    }

    protected override bool SetInput(int inputIndex, float value) => false;
}