using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC1DConfig", order = 1, fileName = "WFC1DConfig"), Serializable]

public class WFC1DConfig :WFC2DConfig
{
    public override WFCTile CreateNodeTile()
    {
        WFC1DTile nodeTile = CreateInstance<WFC1DTile>();
        nodeTile.tileName = "WFC1D tile";
        nodeTile.InitDataStructures();
        nodeTile.tileId = GUID.Generate().ToString();
        wfcTilesList.Add(nodeTile);
        AssetDatabase.AddObjectToAsset(nodeTile, this);
        AssetDatabase.SaveAssets();
        return nodeTile;
    }
}
