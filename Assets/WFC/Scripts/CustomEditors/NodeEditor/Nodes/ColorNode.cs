using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorNode : Node
{
    public Color color;
    public Port InputPort;
    public Action<ColorNode> OnNodeSelection;

    protected void CreateInputPort()
    {
        InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        InputPort.portName = "Color";
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelection is not null) OnNodeSelection.Invoke(this);
    }
}