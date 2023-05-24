using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC3DConfig", order = 3, fileName = "WFC3DConfig"), Serializable]
public class WFC3DConfig : WFCConfig
{
    public override WFCTile CreateNodeTile()
    {
        WFC3DTile nodeTile = CreateInstance<WFC3DTile>();
        nodeTile.tileName = "WFC3D tile";
        nodeTile.InitDataStructures();
        nodeTile.tileId = GUID.Generate().ToString();
        wfcTilesList.Add(nodeTile);
        AssetDatabase.AddObjectToAsset(nodeTile, this);
        AssetDatabase.SaveAssets();
        return nodeTile;
    }

    public override EditorManager getEditorManager()
    {
        return new Editor3DManager(this);
    }

    public override IWFCSpawner GetWfcSpawner()
    {
        throw new NotImplementedException();
    }
}