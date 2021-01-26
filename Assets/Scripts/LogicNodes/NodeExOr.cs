using UnityEngine;

public class NodeExOr : BasicNode
{
    public override string Name => "OR Gate";

    int GetActiveCount()
    {
        int result = 0;
        foreach (float f in inputs)
        {
            result += f >= 0.5f || f <= -0.5f ? 1 : 0;
        }
        return result;
    }

    protected override void Calculate() =>
        SendSignal(0, GetActiveCount() & 1);
}