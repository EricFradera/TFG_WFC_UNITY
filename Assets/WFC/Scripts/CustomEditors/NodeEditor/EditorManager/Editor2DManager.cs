using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class Editor2DManager : EditorManager
{
    public Editor2DManager(WFC2DConfig config)
    {
        this.config = config;
    }


    public override NodeComponent createNodeView(object obj)
    {
        switch (obj)
        {
            case WFC2DTile wfc2DTile:
            {
                var nodeComponent = new Node2dComponent(wfc2DTile);
                return nodeComponent;
            }
            case InputCodeData inputCodeData:
            {
                var helperNode = new StringCodeNode(inputCodeData);
                return helperNode;
            }
            default: return null;
        }
    }

    protected override int dirHelper(string dirName)
    {
        return dirName switch
        {
            "up" => 0,
            "right" => 1,
            "down" => 2,
            "left" => 3,
            _ => -1
        };
    }
}