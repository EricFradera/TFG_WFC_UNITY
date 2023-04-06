using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC2DConfig", order = 3, fileName = "WFC2DConfig"), Serializable]
public class WFC2DConfig : WFCConfig
{
    public override WFCTile CreateNodeTile()
    {
        WFC2DTile nodeTile = CreateInstance<WFC2DTile>();
        nodeTile.tileName = "WFC2D tile";
        nodeTile.InitDataStructures();
        nodeTile.tileId = GUID.Generate().ToString();
        wfcTilesList.Add(nodeTile);
        AssetDatabase.AddObjectToAsset(nodeTile, this);
        AssetDatabase.SaveAssets();
        return nodeTile;
    }
}