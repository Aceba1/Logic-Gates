using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public void Start()
    {
        Time.fixedDeltaTime = 0.2f;
    }

    //public List<LogicGate> gates;

    //public static LogicGate CreateLogicGate(System.Func<bool> Step)
    //{

    //}

    public int cycle { get; private set; } // Increment every step
    public int nodeStep; //TEMPORARY: DEBUG

    //TODO: Implement STRUCT?
    private HashSet<LogicNode> gates = new HashSet<LogicNode>();

    private List<LogicNode> activeOdd = new List<LogicNode>();
    private List<LogicNode> activeEven = new List<LogicNode>();
    //private Queue<LogicNode> waiting = new Queue<LogicNode>();

    private Stack<LogicNode> firstWait = new Stack<LogicNode>();
    private Stack<LogicNode> lastWait = new Stack<LogicNode>();


    public void MarkPriority(LogicNode node) => firstWait.Push(node);
    public void MarkWaiting(LogicNode node) => lastWait.Push(node);

    public void MarkActive(LogicNode node) =>
        ((cycle & 1) == 0 ? activeEven : activeOdd).Add(node);

    public bool MarkInactive(LogicNode node) =>
        activeEven.Remove(node) || activeOdd.Remove(node);

    //public void UnlistWait(LogicNode node)
    //{
    //    waiting.
    //}

    public void Add(LogicNode node)
    {
        gates.Add(node);
        node.manager = this;
        MarkActive(node);
    }

    public void Remove(LogicNode node)
    {
        gates.Remove(node);
        node.manager = null;
        MarkInactive(node);
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
            if (currentActive.Count == 0) continue; // Nothing wants to change

            cycle++;
            nodeStep = 0;

            foreach (var node in currentActive)
                node.TryCalculate();

            currentActive.Clear();

            while (firstWait.Count + lastWait.Count != 0)
            {
                if (firstWait.Count != 0)
                    firstWait.Pop().TryCalculate();
                else
                    lastWait.Pop().TryCalculate();
            }
        }
    }

    private void FixedUpdate()
    {
        //foreach (var gate in gates)
        //{
        //    gate.PreStep();
        //}
    }
}
