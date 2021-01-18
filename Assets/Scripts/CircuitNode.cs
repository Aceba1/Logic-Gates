using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitNode : MonoBehaviour
{
    [SerializeField]
    private string nodeName;

    [SerializeField]
    private bool isOutput;

    [SerializeField]
    private ValueType type;

    public List<CircuitNode> targets = new List<CircuitNode>();

    //public int Index => 
    public string NodeName => nodeName;
    public bool IsOutput => isOutput;
    public ValueType Type => type;

    private static CircuitNode held;
    private static CircuitNode hover;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum ValueType : byte
    {
        Bool, // [0],[1]
        Range, // [-1, 1]
        Value, // [-,+]
        //Vector, // [Vector4]
    }

    private void OnMouseEnter()
    {
        Debug.Log("Node Enter");
        if (held != this)
            hover = this;
    }

    private void OnMouseExit()
    {
        Debug.Log("Node Exit");
        if (hover == this) hover = null;
    }

    private void OnMouseDown()
    {
        Debug.Log("Node Down");
        hover = null;
        held = this;
    }

    private void OnMouseDrag()
    {
        //Debug.Log("Node Drag");
    }

    private void OnMouseUp()
    {
        Debug.Log("Node Up");
        if (hover)
        {
            if (isOutput) SetTarget(hover);
            else hover.SetTarget(this);
        }
        held = null;
    }

    internal void SetTarget(CircuitNode target)
    {
        if (target.targets.Count == 0)
        { 
            Debug.Log("Connect!");
            targets.Add(target);
            target.targets.Add(this);
        }
    }

    internal void DisconnectTarget(CircuitNode target)
    {

    }

    private void OnDrawGizmos()
    {
        if (held == this)
            Gizmos.DrawLine(transform.position, hover ? hover.transform.position : Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward));
        if (hover == this)
            Gizmos.DrawWireSphere(transform.position, 0.2f);

        if (IsOutput && targets.Count != 0)
        {
            Gizmos.color = Color.yellow;
            foreach (var target in targets)
                Gizmos.DrawLine(transform.position + transform.right * 0.5f, 
                    target.transform.position - target.transform.right * 0.5f);
        }
    }
}
