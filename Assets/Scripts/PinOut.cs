using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinOut : WirePin
{
    public override bool IsOutput => true;

    public List<WirePin> targets = new List<WirePin>();

    public override Vector3 GetWorldPos() =>
        transform.position + transform.rotation * new Vector3(.5f, 0f, .45f);
    
    private void OnMouseEnter()
    {
        if (held == null)
            visual.material.color = visual.material.color * 2f;
    }

    private void OnMouseExit()
    {
        if (held == null)
            visual.material.color = visual.material.color / 2f;
    }

    //TODO: Figure out how to visualize line drawing
    private void OnMouseDown()
    {
        // Create new connection
        hover = null;
        OnMouseExit();
        held = this;
        line.enabled = true;
    }

    private void OnMouseUp()
    {
        if (hover) // Try to connect to hover
        {
            if (hover.source != null)
                hover.source.Disconnect(hover);
            Connect(hover);
        }
        held = null;
        line.enabled = false;
    }

    private void OnMouseDrag()
    {
        if (line.enabled)
            line.Route(GetWorldPos(), 
                hover ? hover.GetWorldPos() : WireLine.GetWorldMousePos());
    }

    public void Connect(PinIn target)
    {
        target.source = this;
        targets.Add(target);

        module.core.WireTarget(index, target.GetSignal());
        target.line.SetColor(color);

        module.core.MarkActive();
    }

    public void Disconnect(PinIn target)
    {
        target.source = null;
        targets.Remove(target);

        if (!module.core.RemoveWire(index, target.GetSignal()))
            Debug.LogError("Failed to remove wire!");
    }
}
