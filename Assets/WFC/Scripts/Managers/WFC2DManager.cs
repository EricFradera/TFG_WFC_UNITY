using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

public class WFC2DManager : WFCManager
{
    public WFC2DManager(WFCConfig config) : base(config)
    {
    }

    public override WFCTile CreateNodeTile()
    {
        WFC2DTile nodeTile = ScriptableObject.CreateInstance<WFC2DTile>();
        nodeTile.tileName = "WFC2D tile";
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
        return new Editor2DManager(this);
    }

    public override IWFCSpawner GetWfcSpawner()
    {
        throw new System.NotImplementedException();
    }
}