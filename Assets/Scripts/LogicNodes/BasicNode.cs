using System;

public abstract class BasicNode : LogicNode
{
    // Sealing prohibits further overriding
    public sealed override byte Inputs { get => inputSize; set => ResizeInputs(value); }
    public sealed override byte Outputs => 1;

    public int validInputs = 0;
    private byte inputSize;
    public float[] inputs;

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

    public override void Disconnect(int inputIndex)
    {
        base.Disconnect(inputIndex);
        inputs[inputIndex] = 0f;
    }

    public override void PreCalculate()
    {
        validInputs = 0;
    }

    protected sealed override Response SetInput(int index, float value)
    {
        validInputs++;

        if (inputs[index] == value) 
            return validInputs == ConnectedInputs ? Response.Ready : Response.None;
        
        inputs[index] = value;

        return validInputs == ConnectedInputs ? Response.Ready : Response.Wait;
    }
}