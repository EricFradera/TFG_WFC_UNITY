using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using WFC;


public class NodeComponent : Node
{
    //This has to be changed for an abstraction of the type WFC2DTile
    public WFC2DTile tile;
    public readonly Port[] input;
    public readonly Port[] output;
    public Action<NodeComponent> OnNodeSelection;

    private String[] portNames =
    {
        "up", "right", "down", "left"
    };

    public NodeComponent(WFCTile tile)
    {
        Debug.Log("Gets rebuild");
        this.tile = (WFC2DTile)tile;
        this.viewDataKey = tile.tileId;
        input = new Port[4];
        output = new Port[4];
        //this,viewDataKey=node.guid;
        style.left = tile.nodeData.position.x;
        style.top = tile.nodeData.position.y;
        for (int i = 0; i < 4; i++)
        {
            CreateInputPort(i);
            CreateOutputPort(i);
        }
    }

    private void CreateInputPort(int dir)
    {
        //Here I need to check based on what type of tile it is. For now We are working in a 2D space so no checking
        input[dir] = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input[dir].portName = portNames[dir];
        inputContainer.Add(input[dir]);
    }

    private void CreateOutputPort(int dir)
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