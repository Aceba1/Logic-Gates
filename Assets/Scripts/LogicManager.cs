using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    //public List<LogicGate> gates;

    //public static LogicGate CreateLogicGate(System.Func<bool> Step)
    //{

    //}

    public int cycle { get; private set; } // Increment every step

    //TODO: Implement STRUCT?
    private HashSet<LogicNode> gates = new HashSet<LogicNode>();

    private List<LogicNode> activeOdd = new List<LogicNode>();
    private List<LogicNode> activeEven = new List<LogicNode>();
    private Queue<LogicNode> waiting = new Queue<LogicNode>();


    public void MarkWaiting(LogicNode node) => waiting.Enqueue(node);

    public void MarkActive(LogicNode node)
    {
        // next-Active
        (cycle % 2 == 0 ? activeOdd : activeEven).Add(node);
    }

    //public void UnlistWait(LogicNode node)
    //{
    //    waiting.
    //}

    public void Add(LogicNode node)
    {
        gates.Add(node);
    }

    public void Remove(LogicNode node)
    {
        gates.Remove(node);
    }

    void OnEnable()
    {
        //gates = new List<LogicGate>();

        // Disabling object removes coroutine:
        // - https://docs.unity3d.com/Manual/Coroutines.html
        StartCoroutine(LateFixedUpdate());
    }

    private void OnDisable()
    {
        //gates.Clear();
    }

    private IEnumerator LateFixedUpdate()
    {
        while (true) //enabled
        {
            yield return new WaitForFixedUpdate(); // Can be set to own cycle

            //Step here
            var currentActive = (cycle & 1) == 0 ? activeEven : activeOdd;

            foreach (var node in currentActive)
            {
                node.ForceCalculate(cycle, this);
            }

            currentActive.Clear();

            while (waiting.Count != 0)
            {
                var node = waiting.Dequeue();
                node.ForceCalculate(cycle, this);
            }
            cycle++;
        }
    }

    private void FixedUpdate()
    {
        foreach (var gate in gates)
        {
            gate.PreStep();
        }
    }
}
