using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

public class WFC3DManager : WFCManager
{
    public WFC3DManager(WFCConfig config) : base(config)
    {
    }
    public override WFCTile CreateNodeTile()
    {
        WFC3DTile nodeTile = ScriptableObject.CreateInstance<WFC3DTile>();
        nodeTile.tileName = "WFC3D tile";
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
        return new Editor3DManager(this);
    }

    
}