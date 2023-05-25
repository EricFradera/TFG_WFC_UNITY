using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = System.Numerics.Vector2;

public class StringCodeNode : NodeComponent
{
    public StringCodeData codeData;

    public StringCodeNode(InputCodeData codeData)
    {
        this.codeData = (StringCodeData)codeData;
        this.viewDataKey = codeData.uid;
        this.title = "String code node";
        portNames = new[] { "code" };
        input = new Port[1];
        style.left = codeData.nodeData.x;
        style.top = codeData.nodeData.y;
        CreateInputPort();
        //Here we add the rest of stuf
        Label titleLabel = this.Q<Label>("title-label");
        titleLabel.bindingPath = "code";
        titleLabel.Bind(new SerializedObject(codeData));
    }

    private void CreateInputPort()
    {
        input[0] = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input[0].portName = "Color";
        inputContainer.Add(input[0]);
    }

    protected override void setNodePos(float x, float y)
    {
        codeData.nodeData.x = x;
        codeData.nodeData.y = y;
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelection is not null) OnNodeSelection.Invoke(this);
    }
}