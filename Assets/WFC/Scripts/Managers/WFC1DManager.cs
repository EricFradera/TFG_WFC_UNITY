using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

public class WFC1DManager : WFCManager
{
    public WFC1DManager(WFCConfig config) : base(config)
    {
    }

    public override WFCTile CreateNodeTile()
    {
        WFC1DTile nodeTile = ScriptableObject.CreateInstance<WFC1DTile>();
        nodeTile.tileName = "WFC1D tile";
        nodeTile.InitDataStructures();
        nodeTile.tileId = GUID.Generate().ToString();
        wfcConfig.wfcTilesList.Add(nodeTile);
        AssetDatabase.AddObjectToAsset(nodeTile, wfcConfig);
        AssetDatabase.SaveAssets();
        return nodeTile;
    }

    public override EditorManager getEditorManager()
    {
        return new Editor1DManager(this);
    }

    public override IWFCSpawner GetWfcSpawner()
    {
        throw new System.NotImplementedException();
    }
}