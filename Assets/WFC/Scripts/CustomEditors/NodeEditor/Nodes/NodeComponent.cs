using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using WFC;


public abstract class NodeComponent : Node
{
    public Port input;
    public Port[] output;
    public Action<NodeComponent> OnNodeSelection;
    protected String[] portNames;

    protected void CreateInputPort()
    {
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input.portName = "input";
        inputContainer.Add(input);
    }

    protected void CreateOutputPort(int dir)
    {
        output[dir] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        output[dir].portName = portNames[dir];
        outputContainer.Add(output[dir]);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        setNodePos(newPos.xMin, newPos.yMin);
    }

    protected abstract void setNodePos(float x, float y);

    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelection is not null) OnNodeSelection.Invoke(this);
    }
}