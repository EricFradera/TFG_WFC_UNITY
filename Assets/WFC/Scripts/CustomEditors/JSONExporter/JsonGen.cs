using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

public class JsonGen
{
    public void GenerateJsonFromTile(WFC2DTile tile, String path)
    {
        string adjacencyConstrains = JsonUtility.ToJson(tile);
        System.IO.File.WriteAllText(
            path + "/Tile" + tile.tileId + ".json", adjacencyConstrains);
    }

    public void GenerateTileFromJson(TextAsset json, String path)
    {
        WFC2DTile tile = ScriptableObject.CreateInstance<WFC2DTile>();
        JsonUtility.FromJsonOverwrite(json.text, tile);
        AssetDatabase.CreateAsset(tile,
            path.Substring(path.LastIndexOf("Assets", StringComparison.Ordinal)) + "/Tile" + tile.tileId + ".asset");
        AssetDatabase.SaveAssets();
    }
}