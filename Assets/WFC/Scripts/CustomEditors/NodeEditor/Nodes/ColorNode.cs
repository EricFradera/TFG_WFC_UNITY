using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorNode : NodeComponent
{
    public Color color;
    public Port InputPort;
    public Action<ColorNode> OnNodeSelection;
    public ColorNodeData data;

    public ColorNode()
    {
    }

    protected void CreateInputPort()
    {
        InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        InputPort.portName = "Color";
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
    }

    protected override void setNodePos(float x, float y) => data.nodeData.position = new Vector2(x, y);

    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelection is not null) OnNodeSelection.Invoke(this);
    }
}