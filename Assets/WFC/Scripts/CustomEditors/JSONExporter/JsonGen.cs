using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using WFC;
using Object = UnityEngine.Object;


[ExecuteInEditMode]
public class JsonGen
{
    /*public void GenerateJsonFromTile(WFC2DTile tile, String path)
    {
        string adjacencyConstrains = JsonUtility.ToJson(tile);
        System.IO.File.WriteAllText(
            path + "/Tile" + tile.tileId + ".json", adjacencyConstrains);
    }*/

    public void GenerateTileFromJson(TextAsset json, String path)
    {
        WFC2DTile tile = ScriptableObject.CreateInstance<WFC2DTile>();
        JsonUtility.FromJsonOverwrite(json.text, tile);
        AssetDatabase.CreateAsset(tile,
            path.Substring(path.LastIndexOf("Assets", StringComparison.Ordinal)) + "/Tile" + tile.tileId + ".asset");
        AssetDatabase.SaveAssets();
    }

    public void GenerateConfigFromJson(string json, string path
    )
    {
        Debug.Log("started");
        /*JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto
        };*/
        WFC2DConfig config = ScriptableObject.CreateInstance<WFC2DConfig>();
        JObject obj = JObject.Parse(json);
        config.configurationName = (string)obj["configurationName"];
        JArray res = (JArray)obj["wfcTilesList"];
        foreach (var tile in res)
        {
        }

        AssetDatabase.CreateAsset(config, "Assets/test.asset");
        AssetDatabase.SaveAssets();
        Debug.Log("succes");
    }

    public void GenerateTileFromJson(string json, string path)
    {
        Debug.Log("started");
        
        Debug.Log("succes");
    }


    public void GenerateFromObject(Object config, String path, string name)
    {
        var serializeOptions = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var adjacencyConstrains = JsonConvert.SerializeObject(config, serializeOptions);

        System.IO.File.WriteAllText(
            path + "/Tile" + name + ".json", adjacencyConstrains);
    }
}