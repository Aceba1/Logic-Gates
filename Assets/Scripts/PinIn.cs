using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PinIn : WirePin
{
    public PinOut source;

    public override bool IsOutput => false;

    LineRenderer line;
    MeshRenderer renderer;
    
    void OnEnable()
    {
        renderer = GetComponent<MeshRenderer>();
        line = GetComponent<LineRenderer>();
    }

    public override Vector3 GetWorldPos() => 
        transform.position + transform.rotation * new Vector3(-.5f, 0f, .445f);

    private void RouteWire()
    {
        // Should make the wire look cleaner?
    }

    private void UpdateWire()
    {
        if (line.enabled)
        {
            line.positionCount = 2;
            line.SetPosition(1, GetWorldPos());
            //Todo: 
            line.SetPosition(0, source.GetWorldPos());

            Vector3 cameraUp = Camera.main.transform.up;

            line.startWidth = ViewWidth(cameraUp, transform.up);
            line.endWidth = ViewWidth(cameraUp, source.transform.up);
        }
    }

    private static float ViewWidth(Vector3 view, Vector3 target)
    {
        return Mathf.Lerp(0.1f, 0.2f, Mathf.Abs(Vector3.Dot(view, target)));
    }

    private void Update()
    {
        line.enabled = source || held == this;
        renderer.enabled = !line.enabled;
        UpdateWire();
    }

    private void OnMouseEnter()
    {
        hover = this;
    }

    private void OnMouseExit()
    {
        hover = null;
    }

    private void OnMouseDown()
    {
        if (source == null) return;
        // Alter this connection, if present
        hover = null;
        held = this;
    }

    private void OnMouseDrag()
    {
        //TODO: Better visualize node altering
    }

    private void OnMouseUp()
    {
        if (held && hover) // Reroute to new PinIn
        {
            source.Connect(hover);
            source.Disconnect(this);
        }
        held = null;
    }
}
