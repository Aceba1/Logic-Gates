using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChipModule : MonoBehaviour
{
    public LogicManager logicManager;

    [HideInInspector]
    public List<CircuitNode> inputs = new List<CircuitNode>();
    [HideInInspector]
    public List<CircuitNode> outputs = new List<CircuitNode>();

    public abstract int InputCount { get; }

    public abstract int OutputCount { get; }

    public abstract void SetInput(int index, float value);

    public abstract void PreStep();

    public abstract void PostStep();

    private void OnEnable()
    {
        foreach (var node in GetComponentsInChildren<CircuitNode>(true))
        {
            if (node.IsOutput) outputs.Add(node);
            else inputs.Add(node);
        }
        
        logicManager.Add(this);
    }

    private void OnDisable()
    {
        logicManager.Remove(this);

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
