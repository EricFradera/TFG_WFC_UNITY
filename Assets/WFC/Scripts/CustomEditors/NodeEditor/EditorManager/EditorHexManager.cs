using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHexManager : EditorManager
{
    public EditorHexManager(WFCManager wfcManager)
    {
        this.wfcConfigManager = wfcManager;
    }

    public override NodeComponent createNodeView(object obj)
    {
        switch (obj)
        {
            case WFCHEXTile wfcHexTile:
            {
                var nodeComponent = new NodeHEXComponent(wfcHexTile);
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
            "Y-" => 0,
            "X+" => 1,
            "Z-" => 2,
            "Y+" => 3,
            "X-" => 4,
            "Z+" => 5,
            _ => -1
        };
    }
}