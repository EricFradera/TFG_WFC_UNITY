using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFCHEXConfig", order = 4, fileName = "WFCHEXConfig"),
 Serializable]
public class WFCHexConfig : WFCConfig
{
    public override WFCTile CreateNodeTile()
    {
        WFCHEXTile nodeTile = CreateInstance<WFCHEXTile>();
        nodeTile.tileName = "WFC2D tile";
        nodeTile.InitDataStructures();
        nodeTile.tileId = GUID.Generate().ToString();
        wfcTilesList.Add(nodeTile);
        AssetDatabase.AddObjectToAsset(nodeTile, this);
        AssetDatabase.SaveAssets();
        return nodeTile;
    }
}