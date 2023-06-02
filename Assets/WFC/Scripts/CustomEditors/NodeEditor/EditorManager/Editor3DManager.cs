using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class Editor3DManager : EditorManager
{
    public Editor3DManager(WFCManager wfcManager)
    {
        this.wfcConfigManager = wfcManager;
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
            "Y+" => 0,
            "Y-" => 1,
            "X+" => 2,
            "X-" => 3,
            "Z+" => 4,
            "Z-" => 5,
            _ => -1
        };
    }
}
