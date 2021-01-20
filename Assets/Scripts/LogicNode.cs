using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class LogicNode
{
    public virtual string Name { get; private set; } = "Unknown Chip";

    public virtual byte Inputs { get; private set; } = 0;
    public virtual byte Outputs { get; private set; } = 0;

    //TODO: Add overloads for different types
    protected abstract bool SetInput(int inputIndex, float value);

    protected abstract void Calculate(LogicManager manager);

    //TODO: Property array
    public LogicManager manager;
    public int lastCycle;

    public Dictionary<int, List<Signal>> OutputSignals = new Dictionary<int, List<Signal>>();

    public bool ContainsWire(int outputIndex, Signal signal) =>
        OutputSignals.TryGetValue(outputIndex, out List<Signal> list) && list.Contains(signal);

    public void WireTarget(int outputIndex, Signal signal) {
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> list))
            list.Add(signal);
        else
            OutputSignals.Add(outputIndex, new List<Signal>() { signal });
    }

    public List<Signal> RemoveWires(int outputIndex) {
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> result))
            OutputSignals.Remove(outputIndex);
        return result;
    }

    //TODO: Add overloads for different types
    public void SendSignal(int outputIndex, float value)
    {
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> wires))
        {
            foreach (var wire in wires)
                SendSignal(wire, value);
        }
    }

    public static void SendSignal(Signal info, float value)
    {
        if (info.target.SetInput(info.targetIndex, value))
            info.target.ForceCalculate();
    }

    public void ForceCalculate()
    {
        if (lastCycle == manager.cycle) 
            return;
        lastCycle = manager.cycle;

        Calculate(manager);
    }

    //Property: abstract class, implement visual overrides, pass changes to node

    public struct Signal
    {
        public byte sourceIndex;
        public byte targetIndex;
        public LogicNode target;

        public override int GetHashCode() =>
            ((sourceIndex << 8) | targetIndex) ^ target.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Signal other && this == other;

        public static bool operator ==(Signal left, Signal right) =>
            left.sourceIndex == right.sourceIndex &&
                left.targetIndex == right.targetIndex &&
                left.target == right.target;

        public static bool operator !=(Signal left, Signal right) => 
            !(left == right);

        //public bool isStable;
    }
}