using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinOut : WirePin
{
    public override bool IsOutput => true;

    public List<WirePin> targets = new List<WirePin>();

    public override Vector3 GetWorldPos() =>
        transform.position + transform.rotation * new Vector3(.5f, 0f, .425f);

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {

    }

    //TODO: Figure out how to visualize line drawing
    private void OnMouseDown()
    {
        // Create new connection
        hover = null;
        held = this;
    }

    private void OnMouseUp()
    {
        if (hover) // Try to connect to hover
        {
            Connect(hover);
        }
        held = null;
    }

    public void Connect(PinIn target)
    {
        target.source = this;
        targets.Add(target);
    }

    public void Disconnect(PinIn target)
    {
        target.source = null;
        targets.Remove(target);
    }
}
