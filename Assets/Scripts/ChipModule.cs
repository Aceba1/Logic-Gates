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
    public List<PinIn> inputs = new List<PinIn>();
    [HideInInspector]
    public List<PinOut> outputs = new List<PinOut>();

    //public abstract int InputCount { get; }

    //public abstract int OutputCount { get; }

    //public abstract void SetInput(int index, float value);

    //public abstract void PreStep();

    //public abstract void PostStep();

    private void OnEnable()
    {
        if (core == null) core = factory.GetNode();
        display = GetComponentInChildren<ChipDisplay>();

        GetComponentsInChildren(true, inputs);
        GetComponentsInChildren(true, outputs);
        byte i = 0;
        foreach (var input in inputs)
        {
            input.module = this;
            input.index = i++;
        }
        i = 0;
        foreach (var output in outputs)
        {
            output.module = this;
            output.index = i++;
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
        //Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        //Debug.Log("Exit");
    }

    private Vector3 Round(Vector3 input) =>
        new Vector3(Mathf.Round(input.x * 2f) * 0.5f, Mathf.Round(input.y * 2f) * 0.5f, Mathf.Round(input.z * 2f) * 0.5f);

    Vector3 startDragPos;

    private void OnMouseDown()
    {
        startDragPos = Camera.main.transform.position - transform.position;
        //Debug.Log("Down");
    }

    private void OnMouseDrag()
    {
        transform.position = Round(Camera.main.transform.position - startDragPos);
    }

    private void OnMouseUp()
    {
        //Debug.Log("Up");
    }

    private void OnDrawGizmos()
    {
        if (core != null)
        {
            Camera camera = Camera.main;
            Vector3 screenPos = camera.WorldToScreenPoint(transform.position + transform.forward * 0.5f - transform.up * 0.5f);
            UnityEditor.Handles.BeginGUI();
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 80, 20), $"{core.lastCycle}");
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y + 20, 80, 20), $"{core.lastIndex}");
            UnityEditor.Handles.EndGUI();
        }
    }
}
