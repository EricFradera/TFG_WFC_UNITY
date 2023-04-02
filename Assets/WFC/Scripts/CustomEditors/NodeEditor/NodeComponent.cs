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
    public Port input;
    public Port output;

    private String[] portNames =
    {
        "up", "right", "down", "left"
    };

    public NodeComponent(WFCTile tile)
    {
        this.tile = (WFC2DTile)tile;
        this.title = tile.tileId.ToString();
        //this,viewDataKey=node.guid;
        style.left = tile.position.x;
        style.top = tile.position.y;
        foreach (var portName in portNames)
        {
            CreateInputPort(portName);
            CreateOutputPort(portName);
        }
    }

    private void CreateInputPort(String portName)
    {
        //Here I need to chack based on what type of tile it is. For now We are working in a 2D space so no checking
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input.portName = portName;
        inputContainer.Add(input);
    }

    private void CreateOutputPort(String portName)
    {
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        output.portName = portName;
        outputContainer.Add(output);
    }


    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        tile.position.x = newPos.xMin;
        tile.position.y = newPos.yMin;
    }
}