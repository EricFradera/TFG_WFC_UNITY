using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class Editor3DManager : EditorManager
{
    public Editor3DManager(WFC3DConfig config)
    {
        this.config = config;
    }

    public override NodeComponent createNodeView(object obj)
    {
        switch (obj)
        {
            case WFC3DTile wfc3DTile:
            {
                var nodeComponent = new Node3dComponent(wfc3DTile);
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
            "y+" => 0,
            "y-" => 1,
            "x+" => 2,
            "x-" => 3,
            "z+" => 4,
            "z-" => 5,
            _ => -1
        };
    }
}