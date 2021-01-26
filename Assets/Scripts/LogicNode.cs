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

    public byte ConnectedInputs { get; protected set; } = 0;

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

    public LogicManager manager;
    //TODO: Make private (Public for debugging)
    public int lastCycle;
    public int lastIndex;

    private bool isWaiting;

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
        
        signal.target.Connect(signal.targetIndex);
        MarkActive();
    }

    public bool TryGetWires(int outputIndex, out List<Signal> wires) =>
        OutputSignals.TryGetValue(outputIndex, out wires);

    public List<Signal> RemoveWires(int outputIndex) {
        if (OutputSignals.TryGetValue(outputIndex, out List<Signal> result))
        {
            OutputSignals.Remove(outputIndex);
            foreach (var signal in result)
            {
                signal.target.Disconnect(signal.targetIndex);
                signal.target.MarkActive();
            }
        }
        return result;
    }

    public bool RemoveWire(int outputIndex, Signal signal)
    {
        var result = OutputSignals.TryGetValue(outputIndex, out List<Signal> list) && list.Remove(signal);
        if (result)
        {
            signal.target.Disconnect(signal.targetIndex);
            signal.target.MarkActive();
        }
        return result;
    }

    /// <summary>
    /// Executes when an output connects to this input.
    /// <para>NOTE: Call base method!</para>
    /// </summary>
    /// <param name="inputIndex">The input index in question</param>
    public virtual void Connect(int inputIndex)
    {
        ConnectedInputs++;
    }
    /// <summary>
    /// Executes when an output disconnects from this input.
    /// <para>NOTE: Call base method!</para>
    /// </summary>
    /// <param name="inputIndex">The input index in question</param>
    public virtual void Disconnect(int inputIndex)
    {
        ConnectedInputs--;
    }

    //TODO: Add overloads for different types
    public void SendSignal(int outputIndex, float value)
    {
        //----TEMPORARY
        if (Mathf.Abs(value) > 0.5f)
            VisualCue |= 0b_10000000;
        else
            VisualCue &= 0b_01111111;
        //----

        if (TryGetWires(outputIndex, out List<Signal> wires))
        {
            foreach (var wire in wires)
                SendSignal(wire, value);
        }
    }

    public static void SendSignal(Signal info, float value)
    {
        if (info.target.SetInput(info.targetIndex, value))
            info.target.MarkPriority();
        else
            info.target.MarkWaiting();
    }

    public void MarkWaiting()
    {
        if (!isWaiting)
        {
            manager.MarkWaiting(this);
            isWaiting = true;
        }
    }

    public void MarkPriority()
    {
        if (lastCycle == manager.cycle)
            manager.MarkActive(this);
        else
            manager.MarkPriority(this);
    }

    public void MarkActive()
    {
        manager.MarkActive(this);
    }

    public void TryCalculate()
    {
        if (lastCycle == manager.cycle)
        {
            if (isWaiting) MarkActive();
        }
        else
        {
            PreCalculate();
            lastCycle = manager.cycle;
            Calculate(manager);
            PostCalculate();
            lastIndex = manager.nodeStep++;
        }
        isWaiting = false;
    }

    public virtual void PreCalculate() { }
    public virtual void PostCalculate() { }

    //Property: abstract class, implement visual overrides, pass changes to node

    public struct Signal
    {
        public Signal(LogicNode target, byte index)
        {
            this.target = target;
            this.targetIndex = index;
        }

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