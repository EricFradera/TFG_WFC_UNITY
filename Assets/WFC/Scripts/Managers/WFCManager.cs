using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using WFC;
using Vector2 = System.Numerics.Vector2;

public abstract class WFCManager
{
    protected WFCConfig wfcConfig;

    protected WFCManager(WFCConfig config)
    {
        this.wfcConfig = config;
    }

    //abstract methods
    public abstract WFCTile CreateNodeTile();
    public abstract EditorManager getEditorManager();


    protected void createNodeData(WFCTile nodeTile)
    {
        //WFCTile needs a manager
        nodeTile.nodeData = ScriptableObject.CreateInstance<nodeData>();
        AssetDatabase.AddObjectToAsset(nodeTile.nodeData, wfcConfig);
        EditorUtility.SetDirty(wfcConfig);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public bool getUseRotations()
    {
        return wfcConfig.useRotations;
    }


    public InputCodeData CreateNodeHelper(Type type)
    {
        InputCodeData codeData;
        if (type == typeof(StringCodeData)) codeData = ScriptableObject.CreateInstance<StringCodeData>();
        else codeData = ScriptableObject.CreateInstance<InputCodeData>();
        AssetDatabase.AddObjectToAsset(codeData.Init(), wfcConfig);
        wfcConfig.nodeHelpers.Add(codeData);
        AssetDatabase.AddObjectToAsset(codeData, wfcConfig);
        EditorUtility.SetDirty(wfcConfig);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return codeData;
    }

    public void DeleteNodeHelper(InputCodeData data)
    {
        data.nodeData.deleteAllRelFromHelper();
        data.deleteNodeData();
        wfcConfig.nodeHelpers.Remove(data);
        EditorUtility.SetDirty(wfcConfig);
        AssetDatabase.RemoveObjectFromAsset(data);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void DeleteNodeTile(WFCTile tile)
    {
        wfcConfig.wfcTilesList.Remove(tile);
        tile.deleteNodeData();
        AssetDatabase.RemoveObjectFromAsset(tile);
        EditorUtility.SetDirty(wfcConfig);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void AddChild(WFCTile parent, WFCTile child, int dirParent)
    {
        if (parent == null || child == null) return;
        parent.nodeData.addNewRel(dirParent, child);
        EditorUtility.SetDirty(wfcConfig);
        parent.saveData();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void AddHelper(InputCodeData data, WFCTile tile, int dir)
    {
        if (data == null || tile == null) return;
        tile.nodeData.addNewRel(dir, data);
        data.nodeData.addNewRel(dir, tile);
        tile.adjacencyCodes[dir] = data;
        EditorUtility.SetDirty(wfcConfig);
        tile.saveData();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void RemoveChild(WFCTile parent, WFCTile child, int dirParent, int dirChild)
    {
        parent.nodeData.removeRel(dirParent, child);
        EditorUtility.SetDirty(wfcConfig);
        parent.saveData();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void removeHelper(StringCodeData codeData, WFCTile tile, int dirParent)
    {
        tile.adjacencyCodes[dirParent] = null;
        tile.nodeData.removeRel(dirParent, codeData);
        EditorUtility.SetDirty(wfcConfig);
        tile.saveData();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public List<WFCTile> GetChildren(WFCTile parent, int dir)
    {
        return parent.adjacencyPairs[dir] ?? new List<WFCTile>();
    }

    public List<WFCTile> GetWfcTilesList()
    {
        return wfcConfig.wfcTilesList;
    }

    public List<InputCodeData> getNodeHelpersList()
    {
        return wfcConfig.nodeHelpers;
    }
}