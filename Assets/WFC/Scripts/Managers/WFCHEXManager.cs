using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

public class WFCHEXManager : WFCManager
{
    public WFCHEXManager(WFCConfig config) : base(config)
    {
    }

    public override WFCTile CreateNodeTile()
    {
        WFCHEXTile nodeTile = ScriptableObject.CreateInstance<WFCHEXTile>();
        nodeTile.tileName = "HEX tile";
        nodeTile.InitDataStructures();
        nodeTile.tileId = GUID.Generate().ToString();
        wfcConfig.wfcTilesList.Add(nodeTile);
        AssetDatabase.AddObjectToAsset(nodeTile, wfcConfig);
        createNodeData(nodeTile);
        AssetDatabase.SaveAssets();
        return nodeTile;
    }

    public override EditorManager getEditorManager()
    {
        return new EditorHexManager(this);
    }
}