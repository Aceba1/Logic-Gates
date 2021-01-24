using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipModule : MonoBehaviour
{
    public LogicManager logicManager;

    public NodeFactory factory;

    public LogicNode core;

    [HideInInspector]
    public ChipDisplay display;
    [HideInInspector]
    public List<WirePin> inputs = new List<WirePin>();
    [HideInInspector]
    public List<WirePin> outputs = new List<WirePin>();

    //public abstract int InputCount { get; }

    //public abstract int OutputCount { get; }

    //public abstract void SetInput(int index, float value);

    //public abstract void PreStep();

    //public abstract void PostStep();

    private void OnEnable()
    {
        if (core == null) core = factory;
        display = GetComponentInChildren<ChipDisplay>();

        foreach (var node in GetComponentsInChildren<WirePin>(true))
        {
            if (node.IsOutput) outputs.Add(node);
            else inputs.Add(node);
        }
        
        logicManager.Add(core);
    }

    private void OnDisable()
    {
        logicManager.Remove(core);

        inputs.Clear();
        outputs.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((core.VisualCue & 0b_10000000) != 0)
            display.SetColor(display.IconColor * 2f);
        else
            display.SetColor(display.IconColor);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
    }

    private void OnMouseDown()
    {
        Debug.Log("Down");
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag");
    }

    private void OnMouseUp()
    {
        Debug.Log("Up");
    }
}
