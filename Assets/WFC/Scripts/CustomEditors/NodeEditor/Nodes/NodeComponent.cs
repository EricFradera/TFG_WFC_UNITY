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
    //This has to be changed for an abstraction of the type WFC2DTile
    public WFCTile tile;
    public Port[] input;
    public Port[] output;
    public Action<NodeComponent> OnNodeSelection;
    public String[] portNames;
    
    protected void CreateInputPort(int dir)
    {
        input[dir] = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input[dir].portName = portNames[dir];
        inputContainer.Add(input[dir]);
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
        tile.nodeData.position.x = newPos.xMin;
        tile.nodeData.position.y = newPos.yMin;
    }
    
    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelection is not null) OnNodeSelection.Invoke(this);
    }
}