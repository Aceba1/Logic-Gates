using System;

abstract class BasicNode : LogicNode
{
    // Sealing prohibits further overriding
    public sealed override byte Inputs { get => inputSize; set => ResizeInputs(value); }
    public sealed override byte Outputs => 1;

    private int validInputs = 0;
    private byte inputSize;
    protected float[] inputs;

    protected BasicNode(byte inputSize = 2)
    {
        this.inputSize = inputSize;
        inputs = new float[inputSize];
    }

    protected void ResizeInputs(byte newSize)
    {
        if (newSize == inputSize)
            return;

        Array.Resize(ref inputs, newSize);
        inputSize = newSize;
    }

    protected sealed override bool SetInput(int index, float value)
    {
        inputs[index] = value;

        return ++validInputs == inputSize;
    }
}