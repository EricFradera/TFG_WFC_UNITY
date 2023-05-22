using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;

namespace WFC
{
    [System.Serializable]
    public abstract class WFCConfig : ScriptableObject
    {
        public string configurationName;
        public int configurationID;
        public List<WFCTile> wfcTilesList = new List<WFCTile>();
        public List<InputCodeData> nodeHelpers;

        [Rename("Use rotations with new tiles")]
        public bool useRotations;

        public abstract WFCTile CreateNodeTile();


        public InputCodeData CreateNodeHelper(Type type)
        {
            InputCodeData codeData;
            if (type == typeof(StringCodeData)) codeData = ScriptableObject.CreateInstance<StringCodeData>();
            else codeData = ScriptableObject.CreateInstance<InputCodeData>();
            codeData.Init();
            nodeHelpers.Add(codeData);
            AssetDatabase.AddObjectToAsset(codeData, this);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return codeData;
        }

        public void DeleteNodeHelper(InputCodeData data)
        {
            data.nodeData.deleteRelFromHelper();
            nodeHelpers.Remove(data);
            EditorUtility.SetDirty(this);
            AssetDatabase.RemoveObjectFromAsset(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void DeleteNodeTile(WFCTile tile)
        {
            wfcTilesList.Remove(tile);
            AssetDatabase.RemoveObjectFromAsset(tile);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void AddChild(WFCTile parent, WFCTile child, int dirParent, int dirChild)
        {
            if (parent == null || child == null) return;
            parent.adjacencyPairs[dirParent].Add(child);
            parent.nodeData.relationShips.Add(new nodeRelation(dirChild, child, dirChild));
            child.adjacencyPairs[dirChild].Add(parent);
            EditorUtility.SetDirty(this);
            parent.saveData();
            child.saveData();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void AddHelper(InputCodeData data, WFCTile tile, int dir)
        {
            if (data == null || tile == null) return;
            tile.nodeData.relationShips.Add(new nodeRelation(dir, data));
            data.nodeData.relationShips.Add(new nodeRelation(dir, tile));
            tile.adjacencyCodes[dir] = data;
            EditorUtility.SetDirty(this);
            tile.saveData();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        public void RemoveChild(WFCTile parent, WFCTile child, int dirParent, int dirChild)
        {
            parent.adjacencyPairs[dirParent].Remove(child);
            child.adjacencyPairs[dirChild].Remove(parent);
            parent.nodeData.removeRel(dirParent, child, dirChild);
            EditorUtility.SetDirty(this);
            parent.saveData();
            child.saveData();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void removeHelper(StringCodeData codeData, WFC2DTile tile, int dirParent)
        {
            tile.adjacencyCodes[dirParent] = null;
            tile.nodeData.removeRel(dirParent, codeData, 0);
            EditorUtility.SetDirty(this);
            tile.saveData();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public List<WFCTile> GetChildren(WFCTile parent, int dir)
        {
            return parent.adjacencyPairs[dir] ?? new List<WFCTile>();
        }
    }
}