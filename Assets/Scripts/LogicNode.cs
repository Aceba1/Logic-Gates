using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class LogicNode
{
    public virtual string Name => "Unknown Chip";

    public virtual byte Inputs { get; set; } = 0;
    public virtual byte Outputs { get; set; } = 0;

    public byte VisualCue { get; protected set; } = 0;

    /// <summary>
    /// Implementation-dependent, store inputs as they come in
    /// </summary>
    /// <param name="inputIndex">The intended input</param>
    /// <param name="value"></param>
    /// <returns>Whether or not all inputs are satisfied / can safely calculate right now</returns>
    protected abstract bool SetInput(int inputIndex, float value);

    /// <summary>
    /// Using implementation-managed inputs, calls SendSignal(outputIndex, value) for each output
    /// </summary>
    /// <param name="manager">The scope of this node</param>
    protected abstract void Calculate(LogicManager manager);

    //public abstract void Deserialize(string JSON);

    //TODO: Property array
    public LogicManager manager;
    public int lastCycle;

    public Dictionary<int, List<Signal>> OutputSignals = new Dictionary<int, List<Signal>>();

    public bool ContainsWire(int outputIndex) =>
        OutputSignals.ContainsKey(outputIndex);

    public bool ContainsWire(int outputIndex, Signal signal) =>
        OutputSignals.TryGetValue(outputIndex, out List<Signal> list) && list.Contains(signal);

    public void WireTarget(int outputIndex, Signal signal) {
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> list))
            list.Add(signal);
        else
            OutputSignals.Add(outputIndex, new List<Signal>() { signal });
    }

    public bool TryGetWires(int outputIndex, out List<Signal> wires) =>
        OutputSignals.TryGetValue(outputIndex, out wires);

    public List<Signal> RemoveWires(int outputIndex) {
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> result))
            OutputSignals.Remove(outputIndex);
        return result;
    }

    //TODO: Add overloads for different types
    public void SendSignal(int outputIndex, float value)
    {
        if (Mathf.Abs(value) > 0.5f)
            VisualCue |= 0b_10000000;
        else
            VisualCue &= 0b_01111111;
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> wires))
        {
            foreach (var wire in wires)
                SendSignal(wire, value);
        }
    }

    public static void SendSignal(Signal info, float value)
    {
        if (info.target.SetInput(info.targetIndex, value))
            info.target.TryCalculate();
    }

    public void TryCalculate()
    {
        if (lastCycle == manager.cycle) 
            return;
        lastCycle = manager.cycle;

        Calculate(manager);
    }

    //Property: abstract class, implement visual overrides, pass changes to node

    public struct Signal
    {
        public byte targetIndex;
        public LogicNode target;

        public override int GetHashCode() =>
            (targetIndex) ^ target.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Signal other && this == other;

        public static bool operator ==(Signal left, Signal right) =>
            left.targetIndex == right.targetIndex &&
                left.target == right.target;

        public static bool operator !=(Signal left, Signal right) => 
            !(left == right);

        //public bool isStable;
    }
}