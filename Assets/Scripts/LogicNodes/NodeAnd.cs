using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class NodeAnd : LogicNode
{
    public override string Name => "AND Gate";

    public override byte Inputs => 2;
    public override byte Outputs => 1;

    float[] inputs = new float[2];

    int validInputs = 0;

    //TODO: Add variants for different operation types

    protected override void Calculate(LogicManager manager)
    {
        SendSignal(0, Mathf.Min(inputs));
    }

    protected override bool SetInput(int index, float value)
    {
        inputs[index] = value;

        return ++validInputs == Inputs;
    }
}