using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor1DManager : EditorManager
{
    
    public Editor1DManager(WFC1DConfig config)
    {
        this.config = config;
    }
    public override NodeComponent createNodeView(object obj)
    {
        switch (obj)
        {
            case WFC1DTile wfc1DTile:
            {
                var nodeComponent = new Node1dComponent(wfc1DTile);
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
            "right" => 0,
            "left" => 1,
            _ => -1
        };
    }
}
