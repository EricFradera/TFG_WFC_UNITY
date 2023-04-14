using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorNode : NodeComponent
{
    public ColorCodeData codeData;

    public ColorNode(InputCodeData codeData)
    {
        this.codeData = (ColorCodeData)codeData;
        this.viewDataKey = codeData.uid;
        this.title = "Color code node";
        portNames = new[] { "code" };
        input = new Port[1];
        style.left = codeData.nodeData.position.x;
        style.top = codeData.nodeData.position.y;
        CreateInputPort();
        //Here we add the rest of stuff
    }

    private void CreateInputPort()
    {
        input[0] = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input[0].portName = "Color";
        inputContainer.Add(input[0]);
    }

    protected override void setNodePos(float x, float y) => codeData.nodeData.position = new Vector2(x, y);

    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelection is not null) OnNodeSelection.Invoke(this);
    }
}