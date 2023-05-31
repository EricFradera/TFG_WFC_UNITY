using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public abstract class InputCodeData : ScriptableObject
{
    public string uid;
    public string socketName;
    public List<string> socketCodes;
    public bool isSymmetrical;
    public nodeData nodeData;

    public abstract nodeData Init();
    public void deleteNodeData()
    {
        nodeData.deleteAllRelFromTile();
        AssetDatabase.RemoveObjectFromAsset(nodeData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public abstract string getCode(bool inverse);

    protected void GenerateUid() => uid = GUID.Generate().ToString();
}