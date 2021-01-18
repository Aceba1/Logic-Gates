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

    //private int cycle; // Increment every step

    //TODO: Implement STRUCT?
    public HashSet<ChipModule> gates = new HashSet<ChipModule>();

    public void Add(ChipModule logicGate)
    {
        gates.Add(logicGate);
    }

    public void Remove(ChipModule logicGate)
    {
        gates.Remove(logicGate);
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
            foreach (var gate in gates)
            {
                gate.PostStep();
            }
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
