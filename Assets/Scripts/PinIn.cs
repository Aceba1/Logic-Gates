using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinIn : WirePin
{
    public PinOut source;

    public override bool IsOutput => false;

    public LogicNode.Signal GetSignal() =>
        new LogicNode.Signal(module.core, index);

    public override Vector3 GetWorldPos() => 
        transform.position + transform.rotation * new Vector3(-.5f, 0f, .45f);

    private void RouteLine()
    {
        // Should make the wire look cleaner?
    }

    private void UpdateLine()
    {
        line.Route(source.GetWorldPos(), GetWorldPos());
    }

    private void UpdateHeldLine()
    {
        line.Route(source.GetWorldPos(), 
            hover ? hover.GetWorldPos() : WireLine.GetWorldMousePos());
    }

    private void Update()
    {
        bool isHeld = held == this;
        bool isActive = isHeld || source;
        line.enabled = isActive;
        visual.enabled = !isActive;

        if (isHeld) UpdateHeldLine();
        else if (isActive) UpdateLine();
    }

    private void OnMouseEnter()
    {
        visual.material.color = visual.material.color * 2f;
        hover = this;
    }

    private void OnMouseExit()
    {
        visual.material.color = visual.material.color / 2f;
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
        if (held && hover != null && hover != this) // Reroute to new PinIn
        {
            source.Connect(hover);
            source.Disconnect(this);
        }
        held = null;
    }
}
