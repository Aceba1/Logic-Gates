using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WirePin : MonoBehaviour
{
    [SerializeField]
    private string nodeName;

    [SerializeField]
    private ValueType type;

    //public int Index => 
    public string NodeName => nodeName;
    public abstract bool IsOutput { get; }
    public ValueType Type => type;

    protected static WirePin held;
    protected static PinIn hover;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract Vector3 GetWorldPos();

    public enum ValueType : byte
    {
        Bool, // [0],[1]
        Range, // [-1, 1]
        Value, // [-,+]
        //Vector, // [Vector4]
    }

    public static void Connect(PinOut source, PinIn target)
    {
        if (target.source != null)
        {
            if (target.source == source) return;
            target.source.Disconnect(target);
        }
        target.source = source;
        source.Connect(target);
    }
}
