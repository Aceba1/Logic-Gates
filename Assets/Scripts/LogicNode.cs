using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class LogicNode
{

    public virtual int Inputs { get; private set; } = 0;

    public int lastCycle;
    public bool isActive;
    public bool isWaiting;
    public int validInputs;

    //public bool ReceiveSignal(LogicSignal info, float value)
    //{

    //}

    //public bool ReceiveSignal(LogicSignal info, GameObject value)
    //{

    //}

    //public bool ReceiveSignal(LogicSignal info, Vector4 value)
    //{

    //}

    //public bool Calculate()
    //{
            
    //}
}