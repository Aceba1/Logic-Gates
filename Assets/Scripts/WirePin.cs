using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WirePin : MonoBehaviour
{
    [SerializeField]
    private string nodeName;

    [SerializeField]
    private ValueType type;

    [ColorUsage(false, true)]
    public Color color = Color.HSVToRGB(.16f, .5f, 1f);

    //public int Index => 
    public string NodeName => nodeName;
    public abstract bool IsOutput { get; }
    public ValueType Type => type;
    public byte index;

    public ChipModule module;
    public WireLine line;
    protected MeshRenderer visual;

    protected static WirePin held;
    protected static PinIn hover;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<WireLine>();
        visual = GetComponent<MeshRenderer>();
        module = GetComponentInParent<ChipModule>();
        line.SetColor(color);
        visual.material.color = color;
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
}
